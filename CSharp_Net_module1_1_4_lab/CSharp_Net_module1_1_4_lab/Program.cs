using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_1_4_lab
{
    class Program
    {
        // 1) declare enum ComputerType
        enum ComputerType
        {
            Desktop,
            Laptop,
            Server
        }

        // 2) declare struct Computer
        struct Computer
        {
            public ComputerType Type;
            public byte Cores;
            public float HGz;
            public byte Memory;
            public int HDD;
            
            public override string ToString()
            {
            	return this.Type.ToString();
            }
        }

        static void Main(string[] args)
        {
            var desktop = new Computer();
            var laptop = new Computer();
            var server = new Computer();

            desktop.Type = ComputerType.Desktop;
            desktop.Cores = 4;
            desktop.HGz = 2.5F;
            desktop.Memory = 6;
            desktop.HDD = 500;

            laptop.Type = ComputerType.Laptop;
            laptop.Cores = 2;
            laptop.HGz = 1.7F;
            laptop.Memory = 4;
            laptop.HDD = 250;

            server.Type = ComputerType.Server;
            server.Cores = 8;
            server.HGz = 3F;
            server.Memory = 16;
            server.HDD = 2048;

            Computer[][] departments = new Computer[4][] {
            new Computer[] {desktop, desktop, laptop, laptop, server},
            new Computer[] {laptop, laptop, laptop},
            new Computer[] {desktop, desktop, desktop, laptop, laptop},
            new Computer[] {desktop, laptop, server, server}
            };

            var allComputersCount = 0;
            var maxHDD = 0;
            var minCPU = int.MaxValue;
            var minMemory = int.MaxValue;
            var computerWithLargestHDD = string.Empty;
            var computerWithLowestProductivity = string.Empty;

            for (var i=0; i < 4; i++)
            {
                for (var j=0; j < departments[i].Length; j++)
                {
                    // finding computer with largest HDD
                    if (departments[i][j].HDD > maxHDD)
                    {
                        maxHDD = departments[i][j].HDD;
                        computerWithLargestHDD = departments[i][j].ToString();
                    }

                    // finding computer with lowest productivity
                    if (departments[i][j].Cores < minCPU && departments[i][j].Memory < minMemory)
                    {
                        minCPU = departments[i][j].Cores;
                        minMemory = departments[i][j].Memory;
                        computerWithLowestProductivity = departments[i][j].ToString();
                    }

                    // changing memory to 8GB for all desktop computers
                    if (departments[i][j].Type == ComputerType.Desktop)
                    {
                        departments[i][j].Memory = 8;
                    }

                    // computers count
                    allComputersCount++;
                }
            }

            Console.WriteLine($"Total count of computers: {allComputersCount}");
            Console.WriteLine($"Computer with largest HDD: {computerWithLargestHDD}");
            Console.WriteLine($"Computer with lowest productivity: {computerWithLowestProductivity}");
            Console.ReadLine();
        }
 
    }
}
