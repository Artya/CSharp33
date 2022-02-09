using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_7_1_lab
{
    public class Computer
    {
        public int Cores { get; set; }
        public double Frequency { get; set; }
        public int Memory { get; set; }
        public int Hdd { get; set; }

        public Computer()
        {
            this.Cores = default;
            this.Frequency = default;
            this.Memory = default;
            this.Hdd = default;
        }

        public Computer(int cores, double frequancy, int memory, int hdd)
        {
            this.Cores = cores;
            this.Frequency = frequancy;
            this.Memory = memory;
            this.Hdd = hdd;
        }

        public override string ToString()
        {
            return $"{nameof(this.Cores)}: {this.Cores}, {nameof(this.Frequency)}: {this.Frequency}, {nameof(this.Memory)}: {this.Memory}, {nameof(this.Hdd)}: {this.Hdd}";
        }

        public static Computer GetComputerFromString(string computerString)
        {
            if (string.IsNullOrEmpty(computerString))
                return null;

            var newComp = new Computer();

            var propertySeparator = new char[] { ',' };
            var keyValueSeparator = new char[] { ':' };

            var properties = computerString.Split(propertySeparator);

            foreach (var property in properties)
            {
                var keyValue = property.Split(keyValueSeparator);
                
                if (keyValue.Length != 2)
                    continue;

                var key = keyValue[0].Trim();

                switch (key)
                {
                    case nameof(newComp.Cores):
                        newComp.Cores = int.Parse(keyValue[1].Trim());
                        break;

                    case nameof(newComp.Frequency):
                        newComp.Frequency = double.Parse(keyValue[1].Trim());
                        break;

                    case nameof(newComp.Memory):
                        newComp.Memory = int.Parse(keyValue[1].Trim());
                        break;

                    case nameof(newComp.Hdd):
                        newComp.Hdd = int.Parse(keyValue[1].Trim());
                        break;
                }
            }

            return newComp;
        }
    }
}
