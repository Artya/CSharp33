using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Serialization
{
    public static class Helper
    {
        private static XmlSerializer xmlFormatter;
        private static BinaryFormatter binaryFormatter;

        static Helper()
        {
            xmlFormatter = new XmlSerializer(typeof(Student));
            binaryFormatter = new BinaryFormatter();
            
        }

        public static void SerializeToXml(Student student, Stream stream)
        {
            using (stream)
            {
                xmlFormatter.Serialize(stream, student);
            }
        }
        public static Student DeserializeXml(Stream stream)
        {
            var student = xmlFormatter.Deserialize(stream) as Student;

            return student;
        }
        public static void SerializeToBinary(Student student, Stream stream)
        {
            using (stream)
            {
                try
                {
                    binaryFormatter.Serialize(stream, student);
                }
                catch (SerializationException ex)
                {
                    Console.WriteLine($"Error while serializing to binary format, error: {ex.Message}");
                    throw;
                }
            }
        }
        public static Student DeserializeBinary(Stream stream)
        {
            var student = binaryFormatter.Deserialize(stream) as Student;
            return student;
        }
    }
}
