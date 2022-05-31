using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal interface IProductTypeRepository 
     {
        public IEnumerable<ProductType> GetProductTypes();
        public ProductType GetProductType(int id);
        public void UpdateProductType(ProductType productType);
        public void CreateProductType(ProductType productType);
        public void DeleteProductType(int id);
    }
}
