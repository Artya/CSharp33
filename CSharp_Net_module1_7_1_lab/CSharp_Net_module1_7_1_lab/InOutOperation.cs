using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Collections;

namespace CSharp_Net_module1_7_1_lab
{
    public static class InOutOperation
    {
        public static string CurrentPath { get; private set; }
        public static string CurrentFile { get; set; }

        static InOutOperation()
        {
            CurrentPath = Directory.GetCurrentDirectory();
            CurrentFile = "<not set>";
        }

        public static void ShowCurrentIOSettings()
        {
            Console.WriteLine($"Current settings: ");
            Console.WriteLine($"Path: {CurrentPath}");
            Console.WriteLine($"File: {CurrentFile}");
        }

        public static void ChangeLocation(string newPath)
        {
            if (!File.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            CurrentPath = newPath;
        }

        public static string GetNewLocationFromUser()
        {
            Console.WriteLine("Enter new location");
            Console.WriteLine(CurrentPath);

            var input = Console.ReadLine();

            return input;
        }

        public static void WriteData(List<Computer> computers)
        {
            CheckEmptyPath();

            using (var stream = new StreamWriter(Path.Combine(CurrentPath, CurrentFile)))
            {
                foreach (var comp in computers)
                {
                    stream.WriteLine(comp.ToString());
                }

                stream.Close();
            }
        }

        private static void CheckEmptyPath()
        {
            if (string.IsNullOrEmpty(CurrentFile) || string.IsNullOrEmpty(CurrentPath))
            {
                throw new ApplicationException("Current file or current path were not set");
            }
        }

        public static List<Computer> ReadData()
        {
            CheckEmptyPath();

            var newList = new List<Computer>();

            using (var streamReader = new StreamReader(Path.Combine(CurrentPath, CurrentFile), Encoding.UTF8))
            {
                while (true)
                {
                    var currentCompLine = streamReader.ReadLine();

                    if (currentCompLine == null)
                        break;

                    var newComp = Computer.GetComputerFromString(currentCompLine);
                    newList.Add(newComp);
                }

                streamReader.Close();
            }

            return newList;
        }

        public static void WriteZip(List<Computer> computers)
        {
            CheckEmptyPath();

            var zipFileName = Path.Combine(CurrentPath, CurrentFile + ".zip");

            using (var fileStreamWriter = new FileStream(zipFileName, FileMode.OpenOrCreate))
            {
                using (var zipStream = new GZipStream(fileStreamWriter, CompressionLevel.Optimal))
                {
                    WriteComputersToStreamAsBytes(computers, zipStream);

                    zipStream.Close();
                }

                fileStreamWriter.Close();
            }
        }

        private static void WriteComputersToStreamAsBytes(List<Computer> computers, Stream stream)
        {
            foreach (var comp in computers)
            {
                var stringComp = comp.ToString() + Environment.NewLine;
                var bytes = Encoding.UTF8.GetBytes(stringComp);

                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static List<Computer> ReadZip()
        {
            CheckEmptyPath();

            var newComputers = new List<Computer>();

            var zipFileName = Path.Combine(CurrentPath, CurrentFile + ".zip");

            using (var fileStream = new FileStream(zipFileName, FileMode.Open, FileAccess.Read))
            {
                using (var zipStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    var builder = new StringBuilder();

                    var currentByte = zipStream.ReadByte();

                    while (currentByte != -1)
                    {
                        var currentSymbol = (char)currentByte;

                        currentByte = zipStream.ReadByte();

                        if (currentSymbol.ToString() != "\n")
                        {
                            builder.Append(currentSymbol);
                            continue;
                        }

                        var newComp = Computer.GetComputerFromString(builder.ToString());
                        newComputers.Add(newComp);
                        builder.Clear();
                    }

                    zipStream.Close();
                }

                fileStream.Close();
            }

            return newComputers;
        }

        public static async Task<List<Computer>> ReadAsync()
        {

            Console.Write("Please wait paralel executing *");

            var task = Task<List<Computer>>.Run(InOutOperation.ReadAsyncInParalelThread);

            while (!task.IsCompleted)
            {
                Thread.Sleep(500);
                Console.Write("*");
            }

            Console.WriteLine();

            return task.Result;
        }
        public static async Task<List<Computer>> ReadAsyncInParalelThread()
        {
            CheckEmptyPath();

            var newComputers = new List<Computer>();

            using (var streamReader = new StreamReader(Path.Combine(CurrentPath, CurrentFile), Encoding.UTF8))
            {
                while (true)
                {
                    var currentComp = await Task<string>.Run(() =>
                     {
                         Thread.Sleep(1000);
                         return streamReader.ReadLineAsync();
                     });

                    if (currentComp == null)
                        break;

                    var newComp = Computer.GetComputerFromString(currentComp);
                    newComputers.Add(newComp);
                }
                streamReader.Close();
            }

            return newComputers;
        }

        public static MemoryStream WriteToMemoryStream(List<Computer> computers)
        {
            var memoryStream = new MemoryStream();

            WriteComputersToStreamAsBytes(computers, memoryStream);

            return memoryStream;
        }
     
        public static void WriteToFileFromMemoryStream(MemoryStream memoryStream)
        {
            CheckEmptyPath();

            using (var fileStream = new FileStream(Path.Combine(CurrentPath, "FromMemoryStream_" + CurrentFile), FileMode.OpenOrCreate))
            {
                memoryStream.WriteTo(fileStream);
                fileStream.Close();
            }
        }
    }
}
