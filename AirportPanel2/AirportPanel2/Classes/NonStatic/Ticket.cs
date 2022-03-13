namespace AirportPanel2
{
    public class Ticket
    {    
        public Flight Flight { get; private set; }
        public TicketClass TicketClass { get; private set; }
        public Passenger Passenger { get; private set; }
        public double PriceInUAH { get; private set; }
        public Ticket(Flight flight, TicketClass ticketClass, Passenger passenger, double price)
        {
            this.Flight = flight;
            this.TicketClass = ticketClass;
            this.Passenger = passenger;
            this.PriceInUAH = price;
        }

        public static void GetTicketFromUser(Airline airline)
        {
            var passenger = ChooseOrCreatePassengerForTicket(airline);

            airline.Passengers.AddPassenger(passenger);

            var flight = (Flight)UserInteraction.ChooseElemementFromArray(airline.Flights.FlightsArray, "Select flight:");
            var ticketClass = (TicketClass)UserInteraction.ChooseEnumValue("Choose ticket class", typeof(TicketClass));
            var price = UserInteraction.GetDouble("Enter price: ");

            var newTicket = new Ticket(flight, ticketClass, passenger, price);
            airline.Tickets.AddTicket(newTicket);
        }

        private static Passenger ChooseOrCreatePassengerForTicket(Airline airline)
        {
            var choice = (PassengerForTicketChoosingVariant)UserInteraction.ChooseEnumValue("User for ticket: ", typeof(PassengerForTicketChoosingVariant));

            var passenger = (Passenger)default;

            switch (choice)
            {
                case PassengerForTicketChoosingVariant.CreateNew:
                    passenger = Passenger.GetPassengerFromUser();
                    break;

                case PassengerForTicketChoosingVariant.SelectExisting:
                    passenger = (Passenger)UserInteraction.ChooseElemementFromArray(airline.Passengers.PassengersArray, "Choose passenger: ");
                    break;
            }

            return passenger;
        }

        public override string ToString()
        {
            return $"{this.Flight} {this.TicketClass} {PriceInUAH} {Passenger}";
        }

        public static void EditTicket(Ticket ticket)
        {
            if (ticket == null)
                return;

            while (true)
            {
                var fieldToEdit = (TicketEditedFields)UserInteraction.ChooseEnumValue("Choose field to edit:", typeof(TicketEditedFields));

                switch (fieldToEdit)
                {
                    case TicketEditedFields.FinishEditing:
                        return;

                    case TicketEditedFields.TicketClass:
                        ticket.TicketClass = (TicketClass)UserInteraction.ChooseEnumValue($"Current value {ticket.TicketClass}, choose new value:", typeof(TicketClass));
                        break;

                    case TicketEditedFields.Passenger:
                        ticket.Passenger = ChooseOrCreatePassengerForTicket(ticket.Flight.Owner);
                        break;

                    case TicketEditedFields.Price:
                        ticket.PriceInUAH = UserInteraction.GetDouble($"Current value {ticket.PriceInUAH}, enter new value:");
                        break;
                }
            }
        }
    }
}
