using System;

namespace AirportPanel2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lituniaky = new Airline("Lituniaky");
            lituniaky.Welcome();
            lituniaky.FillStartData();

            lituniaky.LetsWok();
        }
    }    
}
