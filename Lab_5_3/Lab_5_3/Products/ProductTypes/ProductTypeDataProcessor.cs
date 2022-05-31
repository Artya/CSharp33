using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal class ProductTypeDataProcessor : IDBObjectDataProcessor
    {
        private static  IProductTypeRepository productTypeRepository= default;

        public string MenuTitle => "PRODUCT TYPES";

        public ProductTypeDataProcessor(IProductTypeRepository repository)
        {
            if (productTypeRepository == null)
                productTypeRepository = repository;
        }

        public static ProductType SelectProductType()
        {
            var productTypes = (List<ProductType>)productTypeRepository.GetProductTypes();
            var productTypeID = UserInteraction.ChooseElementFromList<ProductType>(productTypes, "Choose product type");
            return productTypes[productTypeID];
        }
        public void CreateObject()
        {
            var productType = new ProductType()
            {
                Name = UserInteraction.GetString("Enter product type name")
            };

            productTypeRepository.CreateProductType(productType);
        }

        public void DeleteObject()
        {
            var id = UserInteraction.GetInt("Enter product type ID");
            productTypeRepository.DeleteProductType(id);
        }

        public void EditObject()
        {
            var productType = GetProductTypeByID();

            if (productType == null)
                return;

            productType.Name = UserInteraction.GetString("Enter new product type Name");

            productTypeRepository.UpdateProductType(productType);
        }

        public void ReadObject()
        {
            var productType = GetProductTypeByID();
            Console.WriteLine(productType);
        }

        private static ProductType GetProductTypeByID()
        {
            var id = UserInteraction.GetInt("Enter product type ID");
            
            return GetProductTypeByID(id); 
        }

        public static ProductType GetProductTypeByID(int id)
        {
            return productTypeRepository.GetProductType(id);
        }

        public void ShowAllObjects()
        {
            var productTypes = productTypeRepository.GetProductTypes();
            DatabaseTable.ShowObjectsList(productTypes, "Product types: ");
        }
    }
}
