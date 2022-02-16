using System;
using Airport3.Interfaces;

namespace Airport3
{
    public class Airline
    {
        public readonly string Name;
        public IPanel Panel { get; private set; }

        public Airline(string name)
        {
            this.Name = name;
        }

        public void AddPanel(IPanel panel)
        {
            if (this.Panel == null)
            {
                this.Panel = panel;
                return;
            }

            throw new InvalidOperationException("You already have panel.");
        }
    }
}
