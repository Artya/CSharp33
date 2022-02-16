using System;
using System.Xml.Serialization;

namespace Serialization
{
    [Serializable]
    public class Student
    {
        [XmlIgnore]
        public string FirstName;
        public string LastName;
        [NonSerialized]
        public string Nationality;

        private string Address;
        private string Code;

        public Student() { }
        public Student(string firstName, string lastName, string nationality, string address, string code)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nationality = nationality;
            this.Address = address;
            this.Code = code;
        }

        public override string ToString()
        {
            return $"Fisrt name: {this.FirstName}, last name: {this.LastName}, nationality: {this.Nationality}, address: {this.Address}, code: {this.Code}";
        }
    }
}
