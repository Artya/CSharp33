using System;

namespace AirportPanel2
{
    public class TicketContainer : ITableContainer
    {
        public int FlightWidth { get; set; } = 20;
        public int TicketClassWidth { get; set; } = 10;
        public int PassengerWidth { get; set; } = 30;
        public int PriceWidth { get; set; } = 6;
        private readonly int bordersCount = 5;
        public Ticket[] TicketsArray { get; set; }
        public int Length { get => TicketsArray == null ? 0 : TicketsArray.Length; }
        public int TableWidth { get; private set; }
        public Airline Airline { get; }
        public string TableName => "Tickets";

        public TicketContainer(Airline airline)
        {
            this.Airline = airline;
        }

        public void PrintTableRow(int rowIndex, ConsoleColor borderColor)
        {
            var ticket = this.TicketsArray[rowIndex];

            TablePrinter.WriteCell(ticket.Flight.ToString(), this.FlightWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(ticket.TicketClass.ToString(), this.TicketClassWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(ticket.Passenger.ToString(), this.PassengerWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(ticket.PriceInUAH.ToString("0.00"), this.PriceWidth, borderColor, CellTextLevelling.Right);
            Console.WriteLine();
        }

        public void PrintTableTitle(ConsoleColor borderColor)
        {
            TablePrinter.WriteCell("Flight", this.FlightWidth, borderColor);
            TablePrinter.WriteCell("Class", this.TicketClassWidth, borderColor);
            TablePrinter.WriteCell("Passenger", this.PassengerWidth, borderColor);
            TablePrinter.WriteCell("Price", this.PriceWidth, borderColor);
            Console.WriteLine();
        }
        public void AddTicket(Ticket ticket)
        {
            this.TicketsArray = (Ticket[])ServiceHelper.AddElementToArray(this.TicketsArray, typeof(Ticket), ticket);

            FlightWidth = Math.Max(FlightWidth, ticket.Flight.ToString().Length);
            TicketClassWidth = Math.Max(TicketClassWidth, ticket.TicketClass.ToString().Length);
            PassengerWidth = Math.Max(PassengerWidth, ticket.Passenger.ToString().Length);
            PriceWidth = Math.Max(PriceWidth, ticket.PriceInUAH.ToString().Length);

            RecalculateTotalTableWidth();
        }

        public void WorkWithMenu()
        {
            while (true)
            {
                var operation = (TicketMenu)UserInteraction.ChooseEnumValue("Ticket menu", typeof(TicketMenu));

                switch (operation)
                {
                    case TicketMenu.ReturnToMainMenu:
                        return;

                    case TicketMenu.ShowTickets:
                        TablePrinter.PrintTable(this, ConsoleColor.Yellow);
                        break;

                    case TicketMenu.AddNewTicket:
                        Ticket.GetTicketFromUser(Airline);
                        break;

                    case TicketMenu.DeleteTicket:

                        var ticketToDelete = UserInteraction.ChooseElemementFromArray(this.TicketsArray, "Choose ticket");
                        this.TicketsArray = (Ticket[])ServiceHelper.DeleteElementFromArray(this.TicketsArray, ticketToDelete, typeof(Ticket));

                        break;

                    case TicketMenu.EditTicket:

                        var ticketToEdit = (Ticket)UserInteraction.ChooseElemementFromArray(this.TicketsArray, "Choose ticket");
                        Ticket.EditTicket(ticketToEdit);

                        break;

                    default:
                        Console.WriteLine($"Operation {operation} non implemented");
                        break;
                }
            }
        }

        public void RecalculateTotalTableWidth()
        {
            var sum = bordersCount * Constants.BorderWidth;

            sum += FlightWidth;
            sum += TicketClassWidth;
            sum += PassengerWidth;
            sum += PriceWidth;

            TableWidth = sum;
        }

        public TicketContainer GetTicketsByFlights(FlightsContainer flights)
        {
            if (flights == null ||
                flights.FlightsArray == null ||
                flights.FlightsArray.Length == 0)
            {
                return null;
            }

            var newContainer = new TicketContainer(this.Airline);

            foreach (var ticket in this.TicketsArray)
            {
                if (Array.IndexOf(flights.FlightsArray, ticket.Flight) != -1)
                    newContainer.AddTicket(ticket);
            }

            return newContainer;
        }

        public TicketContainer SearchByPrice()
        {
            var minPrice = UserInteraction.GetDouble("Enter min value");
            var maxPrice = UserInteraction.GetDouble("Enter max value");

            var newContainer = new TicketContainer(this.Airline);

            foreach (var ticket in this.TicketsArray)
            {
                if (ticket.PriceInUAH >= minPrice && ticket.PriceInUAH <= maxPrice)
                    newContainer.AddTicket(ticket);
            }

            return newContainer;
        }

        public TicketContainer SearchByPassengerTextField(SearchTypes searchType)
        {
            var searchedValue = UserInteraction.GetString();

            var newContainer = new TicketContainer(this.Airline);

            foreach (var ticket in this.TicketsArray)
            {
                var addItem = false;

                switch (searchType)
                {
                    case SearchTypes.ByLastName:
                        addItem = ticket.Passenger.LastName == searchedValue;
                        break;

                    case SearchTypes.ByFirstName:
                        addItem = ticket.Passenger.FirstName == searchedValue;
                        break;

                    case SearchTypes.ByFullName:
                        addItem = $"{ticket.Passenger.FirstName} {ticket.Passenger.LastName}" == searchedValue;
                        break;

                    case SearchTypes.ByPassport:
                        addItem = ticket.Passenger.Passport == searchedValue;
                        break;
                }

                if (addItem)
                    newContainer.AddTicket(ticket);
            }

            return newContainer;
        }
    }
}
