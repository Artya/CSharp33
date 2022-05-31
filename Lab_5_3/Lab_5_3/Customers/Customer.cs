using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    class Customer : DatabaseTable
    {
        [VariableFields("Name", "Customer name")]
        public string Name { get; set; }

        [VariableFields("Phone", "Customer phone")]
        public string Phone { get; set; }

        [VariableFields("EMail", "Customer EMail")]
        public string EMail { get; set; }

        [VariableFields("Details", "Customer Details")]
        public string Details { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {this.Phone} {this.EMail} {this.Details}";
        }

        public override Dictionary<string, string> GetFields()
        {
            var fields = new Dictionary<string, string>
            {
                { "Name", this.Name },
                { "Phone", this.Phone },
                { "EMail", this.EMail },
                { "Details", this.Details }
            };
            return fields;
        }
    }
}
