using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace InOutOperation
{
    public static class InOutOperation
    {
        public static string CurrentPath { get; private set; }
        public static string CurrentDirectory { get; private set; }
        public static string CurrentFile { get; set; }

        private static byte[] readFromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream must be not null.");

            var bytesToRead = (int)stream.Length;
            var data = new byte[bytesToRead];
            stream.Read(data, 0, bytesToRead);

            return data;
        }

        public static void ChangeLocation(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path must be not null.");

            var splittedPath = path.Split('/', '\\');
            CurrentPath = path;
            CurrentDirectory = splittedPath[splittedPath.Length - 1];

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public static void CreateDirectory(string directoryName)
        {
            if (directoryName == null)
                throw new ArgumentNullException("directoryName must be not null.");

            Directory.CreateDirectory(directoryName);
        }
        public static void WriteData(byte[] data)
        {
            if (CurrentFile == null)
                throw new InvalidOperationException("Set file name to CurrentFile variable.");

            var fileStream = File.OpenWrite($"{CurrentPath}\\{CurrentFile}");
            fileStream.Write(data);
            fileStream.Close();
        }
        public static byte[] ReadData()
        {
            if (CurrentFile == null)
                throw new InvalidOperationException("Set file name to CurrentFile variable.");

            var fileStream = File.OpenRead($"{CurrentPath}\\{CurrentFile}");
            var data = readFromStream(fileStream);
            fileStream.Close();

            return data;
        }
        public static async Task<byte[]> ReadAsync()
        {
            return await Task.Run(() => 
            {
                var data = ReadData();
                Thread.Sleep(1000);
                return data;
            });
        }
        public static void WriteZip(string zipFilePath)
        {
            if (CurrentFile == null)
                throw new InvalidOperationException("Set file name to CurrentFile variable.");

            if (zipFilePath == null)
                throw new ArgumentNullException("zipFilePath must be not null.");

            ZipFile.CreateFromDirectory(CurrentPath, zipFilePath);
        }
        public static void ExtractZip(string zipFilePath, string extractionPath)
        {
            if (CurrentFile == null)
                throw new InvalidOperationException("Set file name to CurrentFile variable.");

            if (extractionPath == null)
                throw new ArgumentNullException("extractionPath must be not null.");

            if (zipFilePath == null)
                throw new ArgumentNullException("zipFilePath must be not null.");

            if (!Directory.Exists(extractionPath))
                Directory.CreateDirectory(extractionPath);

            ZipFile.ExtractToDirectory(zipFilePath, extractionPath);
        }
        public static void WriteToMemory(MemoryStream memoryStream)
        {
            if (memoryStream == null)
                throw new ArgumentNullException("memoryStream must be not null.");

            memoryStream.Write(ReadData());
            memoryStream.Flush();
            memoryStream.Position = 0;
        }
        public static void WriteToFileFromMemoryStream(MemoryStream memoryStream)
        {
            if (memoryStream == null)
                throw new ArgumentNullException("memoryStream must be not null.");

            var data = readFromStream(memoryStream);
            WriteData(data);
        }
    }
}
