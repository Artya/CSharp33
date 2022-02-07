using System;

namespace Airport2
{
    public class Airline
    {
        public readonly string Name;
        public Panel Panel { get; private set; }

        public Airline(string name)
        {
            this.Name = name;
        }

        public void AddPanel(Panel panel)
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
