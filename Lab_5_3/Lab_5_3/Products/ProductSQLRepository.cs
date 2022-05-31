using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab_5_3
{
    internal class ProductSQLRepository : IProductRepository, ISQLRepository
    {
        public SqlConnection Connection { get;  }
        public string SchemaName => "DBO";
        public string TableName => "Products";

        public ProductSQLRepository(SqlConnection connection)
        {
            this.Connection = connection;
        }
        public Product GetProductFromReader(SqlDataReader reader)
        {
            var currentProduct = new Product();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);

                switch (fieldName)
                {
                    case nameof(Product.ID):
                        currentProduct.ID = (int)reader[i];
                        break;

                    case nameof(Product.Name):
                        currentProduct.Name = (string)reader[i];
                        break;

                    case nameof(Product.Description):
                        currentProduct.Description = (string)reader[i];
                        break;

                    case nameof(Product.Price):
                        currentProduct.Price = (decimal)reader[i];
                        break;

                    case nameof(Product.ProductType):
                        currentProduct.ProductType = ProductTypeDataProcessor.GetProductTypeByID((int)reader[i]); 
                        break;

                    case nameof(Product.Supplier):
                        currentProduct.Supplier = SupplierDataProcessor.GetSupplierByID((int)reader[i]); 
                        break;

                    default:
                        Console.WriteLine($"Field  {fieldName} isn`t in class Product or it filling not implemented");
                        break;
                }
            }

            return currentProduct;
        }
        public void CreateProduct(Product product)
        {
            SQLRepositoryHelper.ExecuteInsertingObject(product, this);
        }

        public void DeleteProduct(int id)
        {
            SQLRepositoryHelper.ExecuteDeletingObject(id, this);
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            var reader = SQLRepositoryHelper.GetSeletionAllReader(this);

            while (reader.Read())
            {
                var product = GetProductFromReader(reader);
                products.Add(product);
            }

            reader.Close();

            return products;
        }

        public Product GetProduct(int id)
        {
            var reader = SQLRepositoryHelper.ExecuteGetObjectByID(id, this);

            if (reader == null)
                return null;

            var product = GetProductFromReader(reader);
            reader.Close();
            return product;
        }

        public void UpdateProduct(Product product)
        {
            SQLRepositoryHelper.ExecuteUpdatingObject(product, this);
        }
    }
}
