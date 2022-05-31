using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_5_3
{
    internal class ProductDataProcessor : IDBObjectDataProcessor
    {
        private static IProductRepository productRepository;
        public string MenuTitle => "PRODUCTS:";

        public ProductDataProcessor(IProductRepository repository)
        {
            if (productRepository == null)       
                productRepository = repository;
        }

        private static Product GetProductByID()
        {
            var id = UserInteraction.GetInt("Enter product ID");

            return GetProductByID(id);
        }

        public static Product GetProductByID(int id)
        {
            return productRepository.GetProduct(id);
        }

        public static List<Product> GetProducts()
        {
            return productRepository.GetProducts().ToList();       
        }

        public void CreateObject()
        {
            var product = new Product()
            {
                Name = UserInteraction.GetString("Enter product name"),
                Description = UserInteraction.GetString("Enter product description"),
                Price = UserInteraction.GetDecimal("Enter price"),
            };

            product.ProductType = ProductTypeDataProcessor.SelectProductType();
            product.Supplier = SupplierDataProcessor.SelectSupplier();

            productRepository.CreateProduct(product);
        }


        public void DeleteObject()
        {
            var id = UserInteraction.GetInt("Enter product ID");
            productRepository.DeleteProduct(id);
        }

        public void EditObject()
        {
            var product = GetProductByID();

            if (product == null)
                return;

            var fields = VariableFields.GetChangingFields(typeof(Product));

            while (true)
            {
                var index = UserInteraction.ChooseElementFromList<VariableFields>(fields, "Choose field to edit");

                if (index == fields.Count - 1)
                {
                    if (product.IsModified)
                    {
                        productRepository.UpdateProduct(product);
                    }
                    return;
                }

                switch (fields[index].FieldName)
                {
                    case "Name":
                        product.Name = UserInteraction.GetString($"Enter new {fields[index]} for {product}");
                        break;

                    case "Description":
                        product.Description = UserInteraction.GetString($"Enter new {fields[index]} for {product}");
                        break;

                    case "Price":
                        product.Price = UserInteraction.GetDecimal($"Enter new {fields[index]} for {product}");
                        break;

                    case "ProductType":
                        product.ProductType = ProductTypeDataProcessor.SelectProductType(); 
                        break;

                    case "Supplier":
                        product.Supplier = SupplierDataProcessor.SelectSupplier();
                        break;
                }

                product.SetModifiedOn();
            }
        }

        public void ReadObject()
        {
            var product = GetProductByID();
            Console.WriteLine(product);
        }

        public void ShowAllObjects()
        {
            var products = productRepository.GetProducts();
            DatabaseTable.ShowObjectsList(products, "Products:");
        }
    }
}
