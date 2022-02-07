using System;
using Airport2.Enums;

namespace Airport2
{
    class Program
    {
        static void Main(string[] args)
        {
            var skyUpAirline = new Airline("SkyUp Airlines");
            var panel = new Panel(skyUpAirline);
            skyUpAirline.AddPanel(panel);

            Helper.PrepareFlights(panel);
            panel.Start();
        }
    }
}
