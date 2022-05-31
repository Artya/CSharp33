using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab_5_3
{
    internal class ProductTypeSQLRepository : IProductTypeRepository, ISQLRepository
    {
        public SqlConnection Connection { get;  }
        public string SchemaName => "DBO";
        public string TableName => "ProductTypes";

        public ProductTypeSQLRepository(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public ProductType GetProductTypeFromReader(SqlDataReader reader)
        {
            var currentProductType = new ProductType();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);

                switch (fieldName)
                {
                    case nameof(ProductType.ID):
                        currentProductType.ID = (int)reader[i];
                        break;

                    case nameof(ProductType.Name):
                        currentProductType.Name = (string)reader[i];
                        break;

                    default:
                        Console.WriteLine($"Field  {fieldName} isn`t in class Product type, or it filling not implemented");
                        break;
                }
            }

            return currentProductType;
        }

        public void CreateProductType(ProductType productType)
        {
            SQLRepositoryHelper.ExecuteInsertingObject(productType, this);
        }

        public void DeleteProductType(int id)
        {
            SQLRepositoryHelper.ExecuteDeletingObject(id, this);
        }

        public ProductType GetProductType(int id)
        {
            var reader = SQLRepositoryHelper.ExecuteGetObjectByID(id, this);

            if (reader == null)
                return null;

            var productType = GetProductTypeFromReader(reader);
            reader.Close();
            return productType;
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            var productTypes = new List<ProductType>();

            var reader = SQLRepositoryHelper.GetSeletionAllReader(this);

            while (reader.Read())
            {
                var currentProductType = GetProductTypeFromReader(reader);
                productTypes.Add(currentProductType);
            }

            reader.Close();

            return productTypes;
        }

        public void UpdateProductType(ProductType productType)
        {
            SQLRepositoryHelper.ExecuteUpdatingObject(productType, this);
        }
    }
}
