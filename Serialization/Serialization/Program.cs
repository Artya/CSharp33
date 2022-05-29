using System;
using System.IO;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var student = new Student("Petro", "Ivanov", "Ukrainian", "Khreschatyk street, 21", "01001");

            Console.WriteLine($"Student: {student}");

            using (FileStream xmlStream = new FileStream("student.xml", FileMode.OpenOrCreate),
                   binStream = new FileStream("student.bin", FileMode.OpenOrCreate))
            {
                Helper.SerializeToXml(student, xmlStream);
                Helper.SerializeToBinary(student, binStream);
            }

            using (FileStream xmlStream = new FileStream("student.xml", FileMode.Open, FileAccess.Read), 
                   binStream = new FileStream("student.bin", FileMode.Open, FileAccess.Read))
            {
                var studentFromXml = Helper.DeserializeXml(xmlStream);
                var studentFromBin = Helper.DeserializeBinary(binStream);

                Console.WriteLine($"Student from xml file: {studentFromXml}");
                Console.WriteLine($"Student from binary file: {studentFromBin}");
            }
        }
    }
}
