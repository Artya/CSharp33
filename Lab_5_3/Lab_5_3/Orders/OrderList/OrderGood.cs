using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal class OrderGood : DatabaseTable
    {
        public Order Order { get; set; }

        [VariableFields("Product", "Product")]
        public Product Product { get; set; }

        [VariableFields("Amount", "Amount")]
        public decimal Amount { get; set; }

        public override Dictionary<string, string> GetFields()
        {
            var fields = new Dictionary<string, string>()
            {
                ["[Order]"] = this.Order.ID.ToString(),
                ["Product"] = this.Product.ID.ToString(),
                ["Amount"]= this.Amount.ToString().Replace(',', '.')
            };

            return fields;
        }

        public override string ToString()
        {
            return $"Product: {this.Product} Amount: {this.Amount}";
        }
    }
}
