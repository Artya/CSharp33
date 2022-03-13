namespace AirportPanel2
{
    public class Gates
    { 
        public Terminals Terminal { get;  }
        public string Name { get;  }

        public Gates(string name, Terminals terminal)
        {
            this.Name = name;
            this.Terminal = terminal;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}