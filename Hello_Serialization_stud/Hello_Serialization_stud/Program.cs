using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Hello_Serialization_stud
{
    class Program
    {
        static void Main(string[] args)
        {
            var student1 = new Student();
            student1.FirstName = "Marco";
            student1.LastName = "Polo";
            student1.Nationality = "Venice";
            student1.SetAddress("Hreschatyk", "10");
            Console.WriteLine(student1);

            var student2 = new Student();
            student2.FirstName = "Cristo";
            student2.LastName = "Columb";
            student2.Nationality = "Spanish";
            student2.SetAddress("Shevchenka", "20");
            Console.WriteLine(student2);

            var students = new List<Student>();
            students.Add(student1);
            students.Add(student2);

            BinarySerialize(students);

            SoapSerialize(students);

            XmlSerialize(student1);
            XmlSerialize(student2);
        }

        public static void BinarySerialize(List<Student> students)
        {
            var binaryFilePath = "binary.bin";
            Console.WriteLine("BinarySerialize ......");

            using (Stream streamWrite = new FileStream(binaryFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IFormatter serializeFormatter = new BinaryFormatter();

                foreach (var student in students)
                {
                    serializeFormatter.Serialize(streamWrite, student);
                }

                streamWrite.Close();
            }
            
            using (Stream streamRead = new FileStream(binaryFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                IFormatter deserializeFormatter = new BinaryFormatter();

                var deserializedStudent = (Student)default;

                do
                {
                    try
                    {
                        deserializedStudent = (Student)deserializeFormatter.Deserialize(streamRead);
                        Console.WriteLine(deserializedStudent);
                    }
                    catch (Exception)
                    {
                        deserializedStudent = null;
                    }
                }
                while (deserializedStudent != null);

                streamRead.Close();
            }
        }

        public static void SoapSerialize(List<Student> students)
        {
            Console.WriteLine("SoapSerialize ......");
            var soapFilePath = "SoapFile.xml";

            using (Stream streamWrite = new FileStream(soapFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                IFormatter serializeFormatter = new SoapFormatter();

                foreach (var student in students)
                {
                    serializeFormatter.Serialize(streamWrite, student);
                }

                streamWrite.Close();
            }

            using (Stream streamRead = new FileStream(soapFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                IFormatter deserializeFormatter = new SoapFormatter();
                var deserializedStudent = (Student)default;

                do
                {
                    try
                    {
                        deserializedStudent = (Student)deserializeFormatter.Deserialize(streamRead);
                        Console.WriteLine(deserializedStudent);
                    }
                    catch (Exception)
                    {
                        deserializedStudent = null;
                    }
                }
                while (deserializedStudent != null);

                streamRead.Close();
            }
        }

        public static void XmlSerialize(Student student)
        {
            Console.WriteLine("XmlSerialize ......");
            var xmlFilePath = "XmlFile.xml";

            using (Stream stream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var serializer = new XmlSerializer(typeof(Student));

                serializer.Serialize(stream, student);

                stream.Close();
            }

            using (Stream streamRead = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(Student));

                var deserializedStudent = (Student)serializer.Deserialize(streamRead);
                Console.WriteLine(deserializedStudent);             

                streamRead.Close();
            }
        }
    }
}

