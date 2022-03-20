using System;

namespace Hello_Serialization_stud
{
    [Serializable] 
    public class Student  
    {
        [System.Xml.Serialization.XmlIgnore]        
        public string FirstName { get; set; }

        public string LastName { get; set;  }

        [NonSerialized]
        public string Nationality;

        private string address;
        private string code;
     
        public void SetAddress(string address, string code)
        {
            this.address = address;
            this.code = code;
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} {this.Nationality} {this.address} {this.code}";
        }
    }
}
