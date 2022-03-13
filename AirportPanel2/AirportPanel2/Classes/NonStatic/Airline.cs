using System;

namespace AirportPanel2
{
    public class Airline
    {
        public string Name { get; }
        public Airport[] AvaliableAirports { get; set; }
        public FlightsContainer Flights { get; set; }
        public PassengerContainer Passengers { get; set; }
        public TicketContainer Tickets { get; set; }
        public Airline(string name)
        {
            this.Name = name;
            this.Flights = new FlightsContainer(this);
            this.Passengers = new PassengerContainer(this);
            this.Tickets = new TicketContainer(this);
        }

        public void AddAirport(Airport airport)
        {
            this.AvaliableAirports = (Airport[])ServiceHelper.AddElementToArray(this.AvaliableAirports, typeof(Airport), airport);
        }

        public void Welcome()
        {
            Console.WriteLine($"Welcome to {this.Name} panel");
        }

        public void FillStartData()
        {
            CreateNewAirport("Zhuliany", new string[] { "A", }, 5);
            CreateNewAirport("Borispil", new string[] { "D", "E", "F" }, 20);
            CreateNewAirport("Prague", new string[] { "B", "C", "D" }, 12);
            CreateNewAirport("Madrid", new string[] { "A", "B", "C" }, 30);
            CreateNewAirport("Paris", new string[] { "G", "K", "L", "M", "N" }, 15);
            CreateNewAirport("Berlin", new string[] { "A", "B", "C", "G", "K", "L", "M", "N" }, 30);

            for (var i = 0; i < 20; i++)
            {
                var newFlight = Flight.CreateNewRandomFlight(this);
                this.Flights.AddFlight(newFlight);
            }

            var maleNames = Constants.MaleFirstNames();
            var femaleNames = Constants.FemaleFirstNames();
            var lastnames = Constants.LastNames();
            var genders = ServiceHelper.GetEnumValues(typeof(Gender));
            var ticketClasses = ServiceHelper.GetEnumValues(typeof(TicketClass));
            var nationalities = ServiceHelper.GetEnumValues(typeof(Nationality));

            var ticketCount = 20;

            var rand = new Random();

            for (var i = 0; i < ticketCount; i++)
            {
                var index = rand.Next(0, genders.Length);
                var gender = (Gender)genders.GetValue(index);

                var name = string.Empty;

                switch (gender)
                {
                    case Gender.Male:
                        index = rand.Next(0, maleNames.Length);
                        name = maleNames[index];
                        break;

                    case Gender.Female:
                        index = rand.Next(0, femaleNames.Length);
                        name = femaleNames[index];
                        break;
                }       

                index = rand.Next(0, lastnames.Length);
                var lastName = lastnames[index];

                var passport = rand.Next(99999, 999999).ToString();

                var dateOfBirth = DateTime.Now.AddYears(-rand.Next(0, 100)).AddMonths(rand.Next(0, 12)).AddDays(rand.Next(0, 31));

                index = rand.Next(0, nationalities.Length);
                var nationality = (Nationality)nationalities.GetValue(index);

                var passenger = new Passenger(name, lastName, nationality, passport, dateOfBirth, gender);
                this.Passengers.AddPassenger(passenger);

                index = rand.Next(0, this.Flights.Length);
                var flight = this.Flights.FlightsArray[index];

                index = rand.Next(0, ticketClasses.Length);
                var ticketClass = (TicketClass)ticketClasses.GetValue(index);

                var price = Math.Round(rand.NextDouble() + rand.Next(100, 99000), 2);

                var ticket = new Ticket(flight, ticketClass, passenger, price);
                this.Tickets.AddTicket(ticket);
            }
        }

        private void CreateNewAirport(string airportName, string[] terminals, int countOfGates)
        {
            var airport = new Airport(airportName);
            this.AddAirport(airport);

            foreach (var terminalName in terminals)
            {
                var terminal = new Terminals(airport, terminalName);
                airport.AddTerminal(terminal);

                for (var i = 0; i < countOfGates; i++)
                {
                    var newGate = new Gates("Gate " + i.ToString(), terminal);
                    terminal.AddGate(newGate);
                }
            }
        }

        public void LetsWok()
        {
            while (true)
            {
                var operation = (MainMenu)UserInteraction.ChooseEnumValue("MAIN MENU:", typeof(MainMenu));

                switch (operation)
                {
                    case MainMenu.Flights:
                        this.Flights.WorkWithMenu();
                        break;

                    case MainMenu.Passengers:
                        this.Passengers.WorkWithMenu();
                        break;

                    case MainMenu.Tickets:
                        this.Tickets.WorkWithMenu();
                        break;

                    case MainMenu.SearchBy:

                        SearchBy();
                        break;

                    case MainMenu.ExitProgram:
                        return;
                }
            }
        }
        private void SearchBy()
        {
            var searchType = (SearchTypes)UserInteraction.ChooseEnumValue("Choose search type:", typeof(SearchTypes));

            switch (searchType)
            {
                case SearchTypes.ReturnToMainMenu:
                    return;

                case SearchTypes.ByFlightNumber:
                    SerchByFlightNumber();
                    break;

                case SearchTypes.ByPrice:
                    SearchByPrice();
                    break;

                case SearchTypes.ByFirstName:
                case SearchTypes.ByLastName:
                case SearchTypes.ByFullName:
                case SearchTypes.ByPassport:
                    SearchByName(searchType);
                    break;

                case SearchTypes.ByDeparturePort:
                case SearchTypes.ByArrivalPort:
                    SearchByAirport(searchType);
                    break;

                default:
                    Console.WriteLine($"Search type {searchType} not implemented");
                    break;
            }
        }
        private void SearchByPrice()
        {
            var color = ConsoleColor.Yellow;

            var tickets = this.Tickets.SearchByPrice();
            var flights = GetFlightsFromTickets(tickets);
            var passengers = GetPassengersFromTickets(tickets);

            TablePrinter.PrintTable(flights, color);
            TablePrinter.PrintTable(tickets, color);
            TablePrinter.PrintTable(passengers, color);
        }

        private FlightsContainer GetFlightsFromTickets(TicketContainer tickets)
        {
            var flights = new FlightsContainer(this);

            foreach (var ticket in tickets.TicketsArray)
            {
                if (flights.FlightsArray == null || Array.IndexOf(flights.FlightsArray, ticket.Flight) == -1)
                    flights.AddFlight(ticket.Flight);
            }

            return flights;
        }

        private PassengerContainer GetPassengersFromTickets(TicketContainer tickets)
        {
            var passengers = new PassengerContainer(this);

            foreach (var ticket in tickets.TicketsArray)
            {
                if (passengers.PassengersArray == null || Array.IndexOf(passengers.PassengersArray, ticket.Passenger) == -1)
                    passengers.AddPassenger(ticket.Passenger);
            }

            return passengers;
        }

        private void SerchByFlightNumber()
        {
            var color = ConsoleColor.Yellow;

            var flights = this.Flights.SearchByFlightNumber();
            var tickets = this.Tickets.GetTicketsByFlights(flights);
            var passengers = GetPassengersFromTickets(tickets);

            TablePrinter.PrintTable(flights, color);
            TablePrinter.PrintTable(tickets, color);
            TablePrinter.PrintTable(passengers, color);
        }

        public void SearchByName(SearchTypes searchType)
        {
            var tickets = this.Tickets.SearchByPassengerTextField(searchType);
            var flights = GetFlightsFromTickets(tickets);
            var passengers = GetPassengersFromTickets(tickets);
            
            var color = ConsoleColor.DarkYellow;

            TablePrinter.PrintTable(flights, color);
            TablePrinter.PrintTable(tickets, color);
            TablePrinter.PrintTable(passengers, color);
        }

        public void SearchByAirport(SearchTypes searchType)
        {
            var flights = this.Flights.SearchByAirport(searchType);
            var tickets = this.Tickets.GetTicketsByFlights(flights);
            var passengers = GetPassengersFromTickets(tickets);

            var borderColor = ConsoleColor.Magenta;

            TablePrinter.PrintTable(flights, borderColor);
            TablePrinter.PrintTable(tickets, borderColor);
            TablePrinter.PrintTable(passengers, borderColor);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
