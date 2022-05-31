using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    class VariableFields : Attribute
    {
        public string FieldName { get;  }
        public string FieldAlias { get; }

        public VariableFields(string fieldAlias, string alias )
        {
            this.FieldName = fieldAlias;
            this.FieldAlias = alias;
        }

        public static List<VariableFields> GetChangingFields(Type type)
        {
            var attributes = new List<VariableFields>();

            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var attribute = (VariableFields)Attribute.GetCustomAttribute(property, typeof(VariableFields));

                if (attribute != null)
                {
                    attributes.Add(attribute);
                }
            }

            attributes.Add(new VariableFields("Finish", "Finish Editing"));

            return attributes;
        }
        public override string ToString()
        {
            return this.FieldAlias;
        }
    }
}
