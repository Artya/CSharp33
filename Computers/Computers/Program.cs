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
        private enum ComputerTypeEnum
        {
            Desktop,
            Laptop,
            Server
        }
        // 2) declare struct Computer
        private struct Computer
        {
            public ComputerTypeEnum ComputerType { get; set; }
            public int CpuCores;
            public double CpuFrequency;
            public int Memory;
            public int Hdd;

            public Computer(ComputerTypeEnum compType)
            {
                ComputerType = compType;

                switch (ComputerType)
                {
                    case ComputerTypeEnum.Desktop:
                        {
                            CpuCores = 4;
                            CpuFrequency = 2.5;
                            Memory = 6;
                            Hdd = 500;
                        }
                        break;
                    case ComputerTypeEnum.Laptop:
                        {
                            CpuCores = 2;
                            CpuFrequency = 1.7;
                            Memory = 4;
                            Hdd = 250;
                        }
                        break;
                    case ComputerTypeEnum.Server:
                        {
                            CpuCores = 8;
                            CpuFrequency = 3;
                            Memory = 16;
                            Hdd = 2048;
                        }
                        break;
                    default:
                        {
                            CpuCores = 0;
                            CpuFrequency = 0;
                            Memory = 0;
                            Hdd = 0;
                        }
                        break;
                }
            }

        }

        static void Main(string[] args)
        {
            // 3) declare jagged array of computers size 4 (4 departments)
            var departments = new Computer[4][];

            // 4) set the size of every array in jagged array (number of computers)
            departments[0] = new Computer[] {new Computer(ComputerTypeEnum.Desktop),
                                            new Computer(ComputerTypeEnum.Desktop),
                                            new Computer(ComputerTypeEnum.Laptop),
                                            new Computer(ComputerTypeEnum.Laptop),
                                            new Computer(ComputerTypeEnum.Server)};
            departments[1] = new Computer[] {new Computer(ComputerTypeEnum.Laptop),
                                            new Computer(ComputerTypeEnum.Laptop),
                                            new Computer(ComputerTypeEnum.Laptop)};
            departments[2] = new Computer[] {new Computer(ComputerTypeEnum.Desktop),
                                            new Computer(ComputerTypeEnum.Desktop),
                                            new Computer(ComputerTypeEnum.Desktop),
                                            new Computer(ComputerTypeEnum.Laptop),
                                            new Computer(ComputerTypeEnum.Laptop)};
            departments[3] = new Computer[] {new Computer(ComputerTypeEnum.Desktop),
                                            new Computer(ComputerTypeEnum.Laptop),
                                            new Computer(ComputerTypeEnum.Server)};
            // 5) initialize array
            // Note: use loops and if-else statements
            for (int i = 0; i < departments.GetLength(0); i++)
            {
                for (int j = 0; j < departments[i].Length; j++)
                {
                    switch (departments[i][j].ComputerType)
                    {
                        case ComputerTypeEnum.Desktop:
                            {
                                departments[i][j].CpuCores = 4;
                                departments[i][j].CpuFrequency = 2.5;
                                departments[i][j].Memory = 6;
                                departments[i][j].Hdd = 500;
                            }
                            break;
                        case ComputerTypeEnum.Laptop:
                            {
                                departments[i][j].CpuCores = 2;
                                departments[i][j].CpuFrequency = 1.7;
                                departments[i][j].Memory = 4;
                                departments[i][j].Hdd = 250;
                            }
                            break;
                        case ComputerTypeEnum.Server:
                            {
                                departments[i][j].CpuCores = 8;
                                departments[i][j].CpuFrequency = 3;
                                departments[i][j].Memory = 16;
                                departments[i][j].Hdd = 2048;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            // 6) count total number of every type of computers
            // 7) count total number of all computers
            // Note: use loops and if-else statements
            // Note: use the same loop for 6) and 7)
            var desktopQuantity = 0;
            var laptopQuantity = 0;
            var serverQuantity = 0;
            var computerQuantity = 0;
            for (int i = 0; i < departments.Length; i++)
            {
                for (int j = 0; j < departments[i].Length; j++)
                {
                    computerQuantity += 1;
                    switch (departments[i][j].ComputerType)
                    {
                        case ComputerTypeEnum.Desktop:
                            desktopQuantity += 1;
                            break;
                        case ComputerTypeEnum.Laptop:
                            laptopQuantity += 1;
                            break;
                        case ComputerTypeEnum.Server:
                            serverQuantity += 1;
                            break;
                    }
                }

            }

            Console.WriteLine($"The total number of {ComputerTypeEnum.Desktop}s in departments is: {desktopQuantity}");
            Console.WriteLine($"The total number of {ComputerTypeEnum.Laptop}s in departments is: {laptopQuantity}");
            Console.WriteLine($"The total number of {ComputerTypeEnum.Server}s in departments is: {serverQuantity}");
            Console.WriteLine($"The total number of all computers in departments is: {computerQuantity}");
            Console.WriteLine();

            // 8) find computer with the largest storage (HDD) - 
            // compare HHD of every computer between each other; 
            // find position of this computer in array (indexes)
            // Note: use loops and if-else statements
            var index1 = 0;
            var index2 = 0;
            var largestStorage = 0;
            for (int i = 0; i < departments.Length; i++)
            {
                for (int j = 0; j < departments[i].Length; j++)
                {
                    if (departments[i][j].Hdd > largestStorage)
                    {
                        largestStorage = departments[i][j].Hdd;
                        index1 = i;
                        index2 = j;
                    }
                }
            }

            Console.WriteLine($"Computer with the largest storage is located in the array at positions {index1}, {index2}");
            Console.WriteLine();

            // 9) find computer with the lowest productivity (CPU and memory) - 
            // compare CPU and memory of every computer between each other; 
            // find position of this computer in array (indexes)
            // Note: use loops and if-else statements
            // Note: use logical oerators in statement conditions
            index1 = 0;
            index2 = 0;
            var lowestProductivity = departments[0][0].CpuCores * departments[0][0].CpuFrequency;
            for (int i = 0; i < departments.Length; i++)
            {
                for (int j = 0; j < departments[i].Length; j++)
                {
                    if ((departments[i][j].CpuCores * departments[i][j].CpuFrequency) < lowestProductivity)
                    {
                        lowestProductivity = departments[i][j].CpuCores * departments[i][j].CpuFrequency;
                        index1 = i;
                        index2 = j;
                    }
                }
            }

            Console.WriteLine($"Computer with the lowest productivity is located in the array at positions {index1}, {index2}"); index1 = 0;
            Console.WriteLine();

            index1 = 0;
            index2 = 0;
            var lowestMemory = departments[0][0].Memory;
            for (int i = 0; i < departments.Length; i++)
            {
                for (int j = 0; j < departments[i].Length; j++)
                {
                    if ((departments[i][j].Memory) < lowestMemory)
                    {
                        lowestMemory = departments[i][j].Memory;
                        index1 = i;
                        index2 = j;
                    }
                }
            }

            Console.WriteLine($"Computer with the lowest memory is located in the array at positions {index1}, {index2}"); index1 = 0;
            Console.WriteLine();

            // 10) make desktop upgrade: change memory up to 8
            // change value of memory to 8 for every desktop. Don't do it for other computers
            // Note: use loops and if-else statements

            for (int i = 0; i < departments.Length; i++)
            {
                for (int j = 0; j < departments[i].Length; j++)
                {
                    if (departments[i][j].ComputerType == ComputerTypeEnum.Desktop) departments[i][j].Memory = 8;
                }
            }

            Console.WriteLine("Memory of desktop computers is upgraded successfully.");

            for (int i = 0; i < departments.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.WriteLine("Department 1:");
                        break;
                    case 1:
                        Console.WriteLine("Department 2:");
                        break;
                    case 2:
                        Console.WriteLine("Department 3:");
                        break;
                    case 3:
                        Console.WriteLine("Department 4:");
                        break;
                    default:
                        break;
                }
                for (int j = 0; j < departments[i].Length; j++)
                {
                    Console.WriteLine($"{departments[i][j].ComputerType}:" +
                                       $"CPU - {departments[i][j].CpuCores} cores, " +
                                       $"{departments[i][j].CpuFrequency} HGz, " +
                                       $"memory - {departments[i][j].Memory} GB, " +
                                       $"HDD - {departments[i][j].Hdd}");
                }
                Console.WriteLine("Press amny key to continue....");
                Console.ReadKey();
                Console.Clear();
            }
        }

    }
}
