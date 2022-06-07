using System;
using System.IO;
using System.Xml.Serialization;

namespace Cryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            const string password = "somestrongpassword";

            var serializer = new XmlSerializer(typeof(Person));

            var petya = new Person()
            {
                Name = "Petya",
                Lastname = "Pupkin"
            };
            var vasya = default(Person);

            var normalData = default(byte[]);
            var signedData = default(byte[]);
            var signedModifiedData = default(byte[]);

            var md5UnsignedHash = new byte[CryptographyHelper.MD5HashLength];
            var md5SignedHash = new byte[CryptographyHelper.MD5HashLength];
            var hmacHash = new byte[CryptographyHelper.HMACSHA256HashLength];

            using (var fileStream = File.Open("person.xml", FileMode.Create))
            {
                serializer.Serialize(fileStream, petya);
            }

            using (var fileStream = File.Open("person.xml", FileMode.Open))
            {
                normalData = new byte[fileStream.Length];
                fileStream.Read(normalData, 0, normalData.Length);
            }

            md5UnsignedHash = CryptographyHelper.MD5Hash(normalData);
            Console.WriteLine($"person.xml hash: {BitConverter.ToString(md5UnsignedHash)}");

            using (var fileStream = File.Open("person.signed.xml", FileMode.Create))
            {
                hmacHash = CryptographyHelper.Sign(normalData, password);

                fileStream.Write(hmacHash);
                fileStream.Write(normalData);
            }

            using (var fileStream = File.Open("person.signed.xml", FileMode.Open))
            {
                signedData = new byte[fileStream.Length];
                fileStream.Read(signedData, 0, signedData.Length);
            }

            md5SignedHash = CryptographyHelper.MD5Hash(signedData);

            Console.WriteLine($"person.signed.xml hash: {BitConverter.ToString(md5SignedHash)}");
            Console.WriteLine($"Is person.xml equals to person.signed.xml? Answer: {CryptographyHelper.MD5HashCheck(signedData, md5UnsignedHash)}");

            using (var fileStream = File.Open("person.signed.xml", FileMode.Open))
            {
                fileStream.Position = CryptographyHelper.HMACSHA256HashLength;

                vasya = (Person)serializer.Deserialize(fileStream);
                vasya.Name = "Vasya";
            }

            using (var fileStream = File.Open("person.signed.modified.xml", FileMode.Create))
            {
                fileStream.Write(hmacHash);
                serializer.Serialize(fileStream, vasya);
            }

            using (var fileStream = File.Open("person.signed.modified.xml", FileMode.Open))
            {
                signedModifiedData = new byte[fileStream.Length - CryptographyHelper.HMACSHA256HashLength];
                fileStream.Position = CryptographyHelper.HMACSHA256HashLength;
                fileStream.Read(signedModifiedData, 0, signedModifiedData.Length);

                Console.WriteLine($"Is person.signed.modified.xml signed right? Answer: {CryptographyHelper.SignCheck(signedModifiedData, password, hmacHash)} ");
            }
        }
    }
}
