﻿using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal class ProductType : DatabaseTable
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.Name.TrimEnd()}";
        }

        public override Dictionary<string, string> GetFields()
        {
            var fields = new Dictionary<string, string>
            {
                ["Name"] = this.Name
            };
            return fields;
        }
    }
}
