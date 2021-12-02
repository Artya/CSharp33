using System;

namespace Computers
{
    class Program
    {
        static void Main(string[] args)
        {
            const int departmentsCount = 4;
            var departments = new Computer[departmentsCount][];

            departments[0] = CreateDepartment(
                ComputerType.Desktop,
                ComputerType.Desktop,
                ComputerType.Laptop,
                ComputerType.Laptop,
                ComputerType.Server
            );

            departments[1] = CreateDepartment(
                ComputerType.Laptop,
                ComputerType.Laptop,
                ComputerType.Laptop
            );

            departments[2] = CreateDepartment(
                ComputerType.Desktop,
                ComputerType.Desktop,
                ComputerType.Desktop,
                ComputerType.Laptop,
                ComputerType.Laptop
            );

            departments[3] = CreateDepartment(
                ComputerType.Desktop,
                ComputerType.Laptop,
                ComputerType.Server,
                ComputerType.Server
            );

            var desktopCount = 0;
            var laptopCount = 0;
            var serverCount = 0;
            foreach (var department in departments)
            {
                foreach (var computer in department)
                {
                    switch(computer.ComputerType)
                    {
                        case ComputerType.Desktop: desktopCount++; break;
                        case ComputerType.Laptop: laptopCount++; break;
                        case ComputerType.Server: serverCount++; break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            Console.WriteLine($"Total computer count: {desktopCount + laptopCount + serverCount}");
            Console.WriteLine($"Desktop computers count: {desktopCount}");
            Console.WriteLine($"Laptop computers count: {laptopCount}");
            Console.WriteLine($"Server computers count: {serverCount}");

            Computer largestHDDComputer = new Computer(default, default, default, default, default);
            foreach (var department in departments)
            {
                foreach (var computer in department)
                {
                    if (largestHDDComputer.HDDGB < computer.HDDGB)
                        largestHDDComputer = computer;
                }
            }

            Console.WriteLine($"Largest computer with type {largestHDDComputer.ComputerType} and storage amount {largestHDDComputer.HDDGB} GB");

            Computer lowwerCPUMemoryComputer = new Computer(default, int.MaxValue, int.MaxValue, int.MaxValue, default);
            foreach (var department in departments)
            {
                foreach (var computer in department)
                {
                    long cpuPower = lowwerCPUMemoryComputer.MemoryGB * lowwerCPUMemoryComputer.CoresCount * lowwerCPUMemoryComputer.CPUFrequencyHZ;
                    long currentCPUPower = computer.MemoryGB * computer.CoresCount * computer.CPUFrequencyHZ;

                    if (cpuPower > currentCPUPower)
                    {
                        lowwerCPUMemoryComputer = computer;
                    }
                }
            }

            for (var i = 0; i < departments.Length; i++)
            {
                for (var j = 0; j < departments[i].Length; j++)
                {
                    var computer = departments[i][j];
                    if (computer.ComputerType == ComputerType.Desktop)
                    {
                        computer.MemoryGB = 8;
                        departments[i][j] = computer;
                    }
                }
            }

            Console.WriteLine($"Lowwer computer with type {lowwerCPUMemoryComputer.ComputerType} and MemoryGB {lowwerCPUMemoryComputer.MemoryGB} and CPUFrequencyHZ {lowwerCPUMemoryComputer.CPUFrequencyHZ}");

            Display(departments);
        }

        private static void Display(Computer[][] departments)
        {
            for (var i = 0; i < departments.Length; i++)
            {
                Console.WriteLine($"Department {i + 1}:");

                for (var j = 0; j < departments[i].Length; j++)
                {
                    var computer = departments[i][j];
                    Console.WriteLine($"Type: {computer.ComputerType}, CPU cores: {computer.CoresCount}, CPU frequency hz: {computer.CPUFrequencyHZ}, Memory GB: {computer.MemoryGB}, HDD GB: {computer.HDDGB}");
                }
            }
        }

        private static Computer[] CreateDepartment(params ComputerType[] computerTypes)
        {
            var result = new Computer[computerTypes.Length];
            for (var index = 0; index < computerTypes.Length; index++)
            {
                var computerType = computerTypes[index];
                result[index] = CreateComputer(computerType);
            }

            return result;
        }
        private static Computer CreateComputer(ComputerType computerType)
        {
            switch (computerType)
            {
                case ComputerType.Desktop:
                    return new Computer(computerType, 4, 2500, 6, 500);
                case ComputerType.Laptop:
                    return new Computer(computerType, 2, 1700, 4, 250);
                case ComputerType.Server:
                    return new Computer(computerType, 8, 3, 16, 2000);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
