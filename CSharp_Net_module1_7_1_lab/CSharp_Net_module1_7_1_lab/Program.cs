using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace CSharp_Net_module1_7_1_lab
{
    class Program
    {
        static void  Main(string[] args)
        {
            InOutOperation.ShowCurrentIOSettings();
            Console.WriteLine("------------------------------------INIT Computers------------------------------------------------");

            var computers = new List<Computer>();

            for (var i = 0; i < 10; i++)
            {
                var cores = i + 1;
                var frequency = 1000 + (i * 10);
                var memory = (i + 1) * 1000;
                var hdd = 100 + i * 10;

                computers.Add(new Computer(cores, frequency, memory, hdd));
            }

            ShowComputers(computers);

            var newDirectory = InOutOperation.GetNewLocationFromUser();
            InOutOperation.ChangeLocation(newDirectory);
            InOutOperation.ShowCurrentIOSettings();

            Console.WriteLine("------------------------------------WriteData------------------------------------------------");
            InOutOperation.CurrentFile = "Computers.txt";
            InOutOperation.ShowCurrentIOSettings();

            InOutOperation.WriteData(computers);

            Console.WriteLine("------------------------------------ReadData------------------------------------------------");
            var newComputers = InOutOperation.ReadData();
            ShowComputers(newComputers);

            Console.WriteLine("------------------------------------WriteZip------------------------------------------------");
            InOutOperation.WriteZip(computers);
            Console.WriteLine("------------------------------------ReadZip------------------------------------------------");
            var zipComputers = InOutOperation.ReadZip();
            ShowComputers(zipComputers);

            Console.WriteLine("------------------------------------ReadAsync------------------------------------------------");
            var asyncComputers = InOutOperation.ReadAsync().Result;
            ShowComputers(asyncComputers);

            Console.WriteLine("------------------------------------WriteToMemoryStream------------------------------------------------");
            var memoryStream = InOutOperation.WriteToMemoryStream(computers);
            Console.WriteLine("------------------------------------WriteToFileFromMemoryStream------------------------------------------------");
            InOutOperation.WriteToFileFromMemoryStream(memoryStream);
            memoryStream.Close();

            Console.WriteLine("------------------------------------Read Memory computers------------------------------------------------");
            InOutOperation.CurrentFile = "FromMemoryStream_" + InOutOperation.CurrentFile;
            var memoryComputers = InOutOperation.ReadData();
            ShowComputers(memoryComputers);
        }

        private static void ShowComputers(List<Computer> computers)
        {
            foreach (var comp in computers)
            {
                Console.WriteLine(comp);
            }
        }
    }
}
