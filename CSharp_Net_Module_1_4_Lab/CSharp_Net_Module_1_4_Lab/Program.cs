using System;

namespace CSharp_Net_Module_1_4_Lab
{
    class Program
    {
        public const byte desktopCores = 4;
        public const byte laptopoCores = 2;
        public const byte serverCores = 8;

        public const double desktopFrequency = 2.5;
        public const double laptopFrequency = 1.7;
        public const double serverFrequency = 3;
        
        public const int desktopMemory = 6;
        public const int maxDesktopMemory = 8;
        public const int laptopMemory = 4;
        public const int serverMemory = 16;
        
        public const int desktopHdd = 500;
        public const int laptopHdd  = 250;
        public const int serverHdd = 2000;

        public struct Cpu 
        { 
            public byte Cores { get;  }
            public double Frequency { get;  }

            public Cpu(byte cores, double frequency)  
            {
                this.Cores = cores; 
                this.Frequency = frequency; 
            }
        }

        public struct Computer
        {
            public CompType compType { get;  }
            public Cpu CPU { get;  }
            public int Memory { get; set;  }
            public int HDD { get; } 

            public Computer(CompType compType)
            {
                this.compType = compType;

                switch (compType)
                    {
                    case CompType.Desktop:
                        this.CPU = new Cpu(desktopCores, desktopFrequency); 
                        this.Memory = desktopMemory;
                        this.HDD = desktopHdd; 
                        break;

                    case CompType.Laptop:
                        this.CPU = new Cpu(laptopoCores, laptopFrequency); 
                        this.Memory = laptopMemory;
                        this.HDD = laptopHdd;
                        break;
                 
                    default: 
                        this.CPU = new Cpu(serverCores, serverFrequency); 
                        this.Memory = serverMemory;
                        this.HDD = serverHdd;
                        break;
                }
            }
            public void UpgradeMemory()
            {
                if (this.compType != CompType.Desktop)
                {
                    return;
                }

                if (Memory == maxDesktopMemory)
                {
                    return;
                }

                Memory = maxDesktopMemory;                                   
            }

            public void RepresentsOfComp()
            {
                Console.WriteLine("Type: " + compType + " CPU: " + CPU.Cores + " cores and  " + CPU.Frequency + "HGz frequency. Memory: " + Memory + "GB. HDD: " + HDD + "GB");    
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World, lets manage org comps!");

            var departmentsCount = 4;
            var organisationDepartments = new Computer[departmentsCount][];

            SetDefaultData(organisationDepartments);
            GetOrgStatistic(departmentsCount, organisationDepartments);

            for (var i = 0; i < departmentsCount; i++)
            {
                for (var j = 0; j < organisationDepartments[i].Length; j++)
                {
                    organisationDepartments[i][j].UpgradeMemory();
                }
            }

            Console.WriteLine("Now we upgraded all desktops, and ...");
            GetOrgStatistic(departmentsCount, organisationDepartments);
        }
        private static void SetDefaultData(Computer[][] organisationDepartments)
        {
            organisationDepartments[0] = new Computer[] { new Computer(CompType.Desktop), new Computer(CompType.Desktop), new Computer(CompType.Laptop), new Computer(CompType.Laptop), new Computer(CompType.Server) };
            organisationDepartments[1] = new Computer[] { new Computer(CompType.Laptop), new Computer(CompType.Laptop), new Computer(CompType.Laptop) };
            organisationDepartments[2] = new Computer[] { new Computer(CompType.Desktop), new Computer(CompType.Desktop), new Computer(CompType.Desktop), new Computer(CompType.Laptop), new Computer(CompType.Laptop) };
            organisationDepartments[3] = new Computer[] { new Computer(CompType.Desktop), new Computer(CompType.Laptop), new Computer(CompType.Server), new Computer(CompType.Server) };
        }

        private static void GetOrgStatistic(int departmentsCount, Computer[][] organisationDepartments)
        {
            Console.WriteLine("In our organisation we have " + departmentsCount + " departments: ");

            var countOfCompTypes = Enum.GetValues(typeof(CompType)).Length;
            var compCountsByType = new int[countOfCompTypes];

            Computer? largestHddComp = null;
            Computer ? lowestProductivityComp = null;

            for (var i = 0; i < departmentsCount; i++)
            {
                Console.WriteLine("Department " + (i + 1));

                foreach (var comp in organisationDepartments[i])
                {
                    comp.RepresentsOfComp();
                    compCountsByType[(int)comp.compType]++;

                    largestHddComp = CheckCurrentCompToLargestHDD(largestHddComp, comp);

                    lowestProductivityComp = CheckCurrentCompToLowestProductivity(lowestProductivityComp, comp);
                }

                Console.WriteLine("------------------END OF DEP ------------------------------");
            }

            var totalComps = 0;
            Console.WriteLine("In all departmenbts we have:");
            for (var i = 0; i < countOfCompTypes; i++)
            {
                Console.WriteLine((CompType)i + "s: " + compCountsByType[i]);
                totalComps += compCountsByType[i];
            }
            Console.WriteLine("Total comps amount:" + totalComps);

            Console.WriteLine("The largest HDD comp is:");
            ((Computer)largestHddComp).RepresentsOfComp();

            Console.WriteLine("The lowest produktivity comp is:");
            ((Computer)lowestProductivityComp).RepresentsOfComp();
        }

        private static Computer? CheckCurrentCompToLowestProductivity(Computer? lowestProductivityComp, Computer comp)
        {
            if (lowestProductivityComp == null)
            {
                return comp;
            }
            
            var locLowProdComp = (Computer)lowestProductivityComp;

            if ((comp.CPU.Cores < locLowProdComp.CPU.Cores && comp.CPU.Frequency < locLowProdComp.CPU.Frequency) && comp.Memory < locLowProdComp.Memory)
            {
                return comp;
            }            

            return lowestProductivityComp;
        }

        private static Computer? CheckCurrentCompToLargestHDD(Computer? largestHddComp, Computer comp)
        {
            if (largestHddComp == null)
            {
                return comp;
            }

            if (comp.HDD > ((Computer)largestHddComp).HDD)
            {
                return comp;
            }       

            return largestHddComp;
        }
    }
}
