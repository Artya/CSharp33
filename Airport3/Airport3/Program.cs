namespace Airport3
{
    class Program
    {
        static void Main(string[] args)
        {
            var airline = new Airline("SkyUp Airlines");
            var panel = new Panel.Panel(airline);
            airline.AddPanel(panel);

            Helper.PrepareFlights(panel);

            panel.Start();
        }
    }
}
