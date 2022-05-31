using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal interface IProductRepository 
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(int id);
        public void UpdateProduct(Product product);
        public void CreateProduct(Product product);
        public void DeleteProduct(int id);
    }
}
