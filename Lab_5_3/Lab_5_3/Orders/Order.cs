using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal class Order : DatabaseTable
    {
        public Customer Customer { get; set; }

        [VariableFields("Status", "Order status")]
        public OrderStatus Status { get; set; }
        
        [VariableFields("Details", "Order details")]
        public string Details { get; set; }

        [VariableFields("Date", "Order Date")]
        public DateTime Date { get; set; }

        [VariableFields("Goods", "Order goods")]
        public List<OrderGood> Goods { get; set; }

        public List<int> DeletedGoods { get; set; }

        public decimal TotalSum { get; private set; } 

        public override string ToString()
        {
            return $"Order {this.ID} from {this.Date} {this.Status} on {this.TotalSum.ToString(".00")} UAH with {(this.Goods== null ? 0 : this.Goods.Count)} goods";
        }

        public override Dictionary<string, string> GetFields()
        {
            var fields = new Dictionary<string, string>()
            {
                ["Customer"] = this.Customer.ID.ToString(),
                ["Status"] = this.Status.ID.ToString(),
                ["Details"] = this.Details,
                ["Date"]= this.Date.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return fields;
        }

        public void AddGoodRow(OrderGood orderGood)
        {
            if (this.Goods == null)
                this.Goods = new List<OrderGood>();

            this.Goods.Add(orderGood);
        }

        public void RemoveGoodRow(OrderGood orderGood)
        {
            if (orderGood == null)
                return;

            var removed = this.Goods.Remove(orderGood);

            if (!removed)
                return;

            if (this.DeletedGoods == null)
                this.DeletedGoods = new List<int>();

            this.DeletedGoods.Add(orderGood.ID);
        }

        public void RecalculateTotalSum()
        {
            this.TotalSum = 0;

            if (this.Goods == null || this.Goods.Count == 0)
                return;

            foreach (var item in this.Goods)
            {
                this.TotalSum += item.Amount * item.Product.Price;
            }
        }
    }
}
