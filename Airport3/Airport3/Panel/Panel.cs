using System;
using Airport3.Interfaces;

namespace Airport3.Panel
{
    public partial class Panel : IPanel
    {
        private Provider provider;
        private Consumer frontend;

        public Airline Airline { get; private set; }

        public Panel(Airline airline)
        {
            if (airline == null)
                throw new ArgumentNullException("airline can't be null.");

            this.Airline = airline;
            this.provider = new Provider();
            this.frontend = new Consumer(this.Airline);
        }

        public void Start()
        {
            this.frontend.SubscribeOn(provider);

            this.provider.SendMessage(new Message(MessageType.Start));
            this.provider.EndTransmition();
        }

        public void AddFlight(Flight flight)
        {
            if (this.provider != null)
                this.provider.Flights.Add(flight);
        }
    }
}
