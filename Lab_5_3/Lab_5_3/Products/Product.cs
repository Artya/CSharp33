using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal class Product : DatabaseTable
    {
        [VariableFields("Name", "Product name")]
        public string Name { get; set; }

        [VariableFields("Description", "Product description")]
        public string Description { get; set; }

        [VariableFields("Price", "Product price")]
        public decimal Price { get; set; }

        [VariableFields("ProductType", "Product type")]
        public ProductType ProductType { get; set; }

        [VariableFields("Supplier", "Product supplier")]
        public Supplier Supplier { get; set; }

        public override string ToString()
        {
            return $"{this.Name.TrimEnd()} Price: {this.Price} Type: {this.ProductType} Supplier: {this.Supplier}";
        }

        public override Dictionary<string, string> GetFields()
        {
            var fields = new Dictionary<string, string>() 
            {
                ["Name"] = this.Name,
                ["Description"] = this.Description,
                ["Price"] = this.Price.ToString().Replace(',', '.'),
                ["ProductType"] = this.ProductType.ID.ToString(),
                ["Supplier"] = this.Supplier.ID.ToString()
            };                         

            return fields;
        }
    }
}
