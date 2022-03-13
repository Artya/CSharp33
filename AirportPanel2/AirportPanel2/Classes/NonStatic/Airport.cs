namespace AirportPanel2
{
    public class Airport
    { 
        public string Name { get; }
        public Terminals [] AvaliableTerminals { get; private set; }

        public Airport(string name)
        {
            this.Name = name;
            this.AvaliableTerminals = default; 
        }

        public void AddTerminal(Terminals terminal)
        {
            this.AvaliableTerminals = (Terminals[])ServiceHelper.AddElementToArray(this.AvaliableTerminals, typeof(Terminals), terminal);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
