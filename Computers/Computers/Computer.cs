namespace Computers
{
    public struct Computer
    {
        public ComputerType ComputerType;
        public int CoresCount;
        public int CPUFrequencyHZ;
        public int MemoryGB;
        public int HDDGB;

        public Computer(ComputerType computerType, int coresCount, int cpuFrequencyHZ, int memoryGB, int hddGB)
        {
            this.ComputerType = computerType;
            this.CoresCount = coresCount;
            this.CPUFrequencyHZ = cpuFrequencyHZ;
            this.MemoryGB = memoryGB;
            this.HDDGB = hddGB;
        }
    }
}
