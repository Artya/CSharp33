using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    class Supplier : DatabaseTable
    {
        [VariableFields("Name", "Supplier name")]
        public string Name { get; set; }

        [VariableFields("Phone", "Supplier phone")]
        public string Phone { get; set; }

        [VariableFields("EMail", "Supplier EMail")]
        public string EMail { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Phone}/{this.EMail}";
        }

        public override Dictionary<string, string> GetFields()
        {
            var fields = new Dictionary<string, string>
            {
                { "Name", this.Name },
                { "Phone", this.Phone },
                { "EMail", this.EMail }
            };

            return fields;
        }
    }
}