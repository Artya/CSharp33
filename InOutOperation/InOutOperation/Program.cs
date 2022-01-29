using System;
using System.IO;
using System.Text;

namespace InOutOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            InOutOperation.ChangeLocation("D:/test");
            InOutOperation.CurrentFile = "file.txt";
            InOutOperation.WriteData(Encoding.ASCII.GetBytes("Some text here"));

            Console.WriteLine($"From async: {Encoding.ASCII.GetString(InOutOperation.ReadAsync().Result)}");
            Console.WriteLine($"From normal: {Encoding.ASCII.GetString(InOutOperation.ReadData())}");

            InOutOperation.WriteZip("D:/compressed.zip");
            InOutOperation.ExtractZip("D:/compressed.zip", $"{InOutOperation.CurrentPath}\\extract");

            var memoryStream = new MemoryStream();
            InOutOperation.WriteToMemory(memoryStream);
            InOutOperation.CurrentFile = "test2.txt";
            InOutOperation.WriteToFileFromMemoryStream(memoryStream);
            memoryStream.Close();

            Clear();
        }

        private static void Clear()
        {
            var files = Directory.GetFiles("D:/test");
            var directories = Directory.GetDirectories("D:/test");

            foreach (var file in files)
                File.Delete(file);

            foreach (var directory in directories)
            {
                files = Directory.GetFiles(directory);

                foreach (var file in files)
                    File.Delete(file);

                Directory.Delete(directory);
            }

            Directory.Delete("D:/test");
            File.Delete("D:/compressed.zip");
        }
    }
}
