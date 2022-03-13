namespace AirportPanel2
{
    public class Terminals
    {
        public Airport Airport { get; }
        public string Name { get; }
        public Gates [] AvaliableGates { get; private set; }

        public Terminals(Airport airport, string name)
        {
            this.Airport = airport;
            this.Name = name;
            this.AvaliableGates = default;
        }

        public void AddGate(Gates gate)
        {
            this.AvaliableGates = (Gates[])ServiceHelper.AddElementToArray(this.AvaliableGates, typeof(Gates), gate);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
