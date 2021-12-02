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

        public enum CompType { desktop, laptop, server};

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
                    case CompType.desktop:
                        this.CPU = new Cpu(desktopCores, desktopFrequency); 
                        this.Memory = desktopMemory;
                        this.HDD = desktopHdd; 
                        break;

                    case CompType.laptop:
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

            public void UpgradeMemory(bool showComment = true)
            {
                if (this.compType != CompType.desktop)
                {
                    if (showComment)
                        Console.WriteLine("Upgrade possible only with desktops, sorry."); 
                    return;
                }

                if (Memory == maxDesktopMemory)
                {
                    if (showComment)
                        Console.WriteLine("This desktop already has max memory " + maxDesktopMemory + " GB");
                    return;
                }

                Memory = maxDesktopMemory;
                
                if (showComment)
                    Console.WriteLine("Desktop memory was upgraded to " + Memory + " GB");                                    
            }

            public void RepresentsOfComp()
            {
                Console.WriteLine("Type: " + compType + " CPU: " + CPU.Cores + " cores and  " + CPU.Frequency + "HGz frequency. Memory: " + Memory + "GB. HDD: " + HDD + "GB");    
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World, lets manage org comps!");

            int departmentsCount;
            Computer[][] organisationDepartments;

            // Хотів уцікавити задачу щоб юзер сам вказував кількість і наповнення департаментів... але часу не вистачило
            //Console.WriteLine("You can choose default data filling, or custom, enter your choise please:");
            //Console.WriteLine("1. Default");
            //Console.WriteLine("2. Custom");
            //Console.WriteLine("Other - exit program....");

            //var input = Console.ReadLine();
            //int choise = int.Parse(input);

            //switch (choise)
            //{
            //    case 1:
            //        SetDefaultData(out departmentsCount, out organisationDepartments);
            //        break;
            //    case 2:
            //        SetCustomData(out departmentsCount, out organisationDepartments);
            //        break;
            //    default:
            //        return;
            //}            

            SetDefaultData(out departmentsCount, out organisationDepartments);
            GetOrgStatistic(departmentsCount, organisationDepartments);

            for (var i = 0; i < departmentsCount; i++)
            {
                for (var j = 0; j < organisationDepartments[i].Length; j++)
                {
                    organisationDepartments[i][j].UpgradeMemory(false);
                }
            }

            Console.WriteLine("Now we upgraded all desktops, and ...");

            GetOrgStatistic(departmentsCount, organisationDepartments);
        }

        private static void SetCustomData(out int departmentsCount, out Computer[][] organisationDepartments)
        {
            Console.WriteLine("So, lets fill our organisation)");
            departmentsCount = 4; 
            organisationDepartments = new Computer[departmentsCount][];
        }

        private static void SetDefaultData(out int departmentsCount, out Computer[][] organisationDepartments)
        {
            departmentsCount = 4;
            organisationDepartments = new Computer[departmentsCount][];
            organisationDepartments[0] = new Computer[] { new Computer(CompType.desktop), new Computer(CompType.desktop), new Computer(CompType.laptop), new Computer(CompType.laptop), new Computer(CompType.server) };
            organisationDepartments[1] = new Computer[] { new Computer(CompType.laptop), new Computer(CompType.laptop), new Computer(CompType.laptop) };
            organisationDepartments[2] = new Computer[] { new Computer(CompType.desktop), new Computer(CompType.desktop), new Computer(CompType.desktop), new Computer(CompType.laptop), new Computer(CompType.laptop) };
            organisationDepartments[3] = new Computer[] { new Computer(CompType.desktop), new Computer(CompType.laptop), new Computer(CompType.server), new Computer(CompType.server) };
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

                    if (largestHddComp == null)
                    {
                        largestHddComp = comp;
                    }
                    else
                    {
                        if (comp.HDD > ((Computer)largestHddComp).HDD)
                        {
                            largestHddComp = comp;
                        }
                    }

                    if (lowestProductivityComp == null)
                    {
                        lowestProductivityComp = comp;
                    }
                    else 
                    {
                        var locLowProdComp = (Computer)lowestProductivityComp;  

                        if ((comp.CPU.Cores < locLowProdComp.CPU.Cores && comp.CPU.Frequency < locLowProdComp.CPU.Frequency) && comp.Memory < locLowProdComp.Memory)
                        {
                            lowestProductivityComp = comp;
                        }    
                    }

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

        
    }
}
