using System;
using Airport2.Enums;

namespace Airport2
{
    public class Panel
    {
        public readonly Airline Airline;
        public Flight[] Flights { get; private set; }

        public Panel(Airline airline)
        {
            this.Airline = airline;
            this.Flights = new Flight[0];
        }

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Welcome to the {this.Airline.Name} panel");
            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                Console.WriteLine("Please, choose the operation:");
                for (var i = 1; i <= Enum.GetValues(typeof(OperationType)).Length; i++)
                    Console.WriteLine(@$"    {i} = {Helper.GetEnumDescription((OperationType)i)}");

                Console.Write($"{Environment.NewLine}> ");

                var operationType = default(int);
                var input = Console.ReadLine();
                var succeed = int.TryParse(input, out operationType);

                if (!succeed)
                {
                    Panel.showError($"There is no operation with id: {input}, try again!");
                    continue;
                }

                switch ((OperationType)operationType)
                {
                    case OperationType.ShowAllFlights:
                        this.ShowFlights();
                        break;
                    case OperationType.ShowAllPassengersForSpecificFlight:
                        this.ShowPassengers(PassengerSearchPattern.All);
                        break;
                    case OperationType.SearchByFlightNumber:
                        this.ShowFlights(FlightSearchPattern.ByFlightNumber);
                        break;
                    case OperationType.SearchByFlightPrice:
                        this.ShowFlights(FlightSearchPattern.ByFlightPrice);
                        break;
                    case OperationType.SearchByFlightArrivalCity:
                        this.ShowFlights(FlightSearchPattern.ByFlightArrivalCity);
                        break;
                    case OperationType.SearchByFlightDepartureCity:
                        this.ShowFlights(FlightSearchPattern.ByFlightDepartureCity);
                        break;
                    case OperationType.SearchByPassengerFirstname:
                        this.ShowPassengers(PassengerSearchPattern.ByPassengerFirstname);
                        break;
                    case OperationType.SearchByPassengerSecondname:
                        this.ShowPassengers(PassengerSearchPattern.ByPassengerSecondname);
                        break;
                    case OperationType.SearchByPassengerPassport:
                        this.ShowPassengers(PassengerSearchPattern.ByPassengerPassport);
                        break;
                    case OperationType.AddFlight:
                        this.AskForFlightInfo();
                        break;
                    case OperationType.EditFlight:
                        this.AskForEditingFlight();
                        break;
                    case OperationType.DeleteFlight:
                        this.AskForDeletingFlight();
                        break;
                    case OperationType.AddPassenger:
                        this.AskForPassengerInfo();
                        break;
                    case OperationType.DeletePassenger:
                        this.AskForDeletingPassenger();
                        break;
                    case OperationType.Exit:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Panel.showError($"There is no operation with id: {input}, try again!");
                        break;
                }
            }
        }

        public void AddFlight(Flight flight)
        {
            this.Flights = Helper.ResizeArray(this.Flights, this.Flights.Length + 1);
            this.Flights[this.Flights.Length - 1] = flight;
        }
        public void RemoveFlight(Flight flight)
        {
            var found = false;
            var index = default(int);

            for (; index < this.Flights.Length; index++)
            {
                if (this.Flights[index] == flight)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return;

            var newFlightArray = new Flight[this.Flights.Length - 1];

            Array.Copy(this.Flights, newFlightArray, index);
            Array.Copy(this.Flights, index + 1, newFlightArray, index, this.Flights.Length - index - 1);

            this.Flights = newFlightArray;
        }

        private static void showError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void showFlight(Flight flight)
        {
            if (flight == null)
                return;

            Console.WriteLine(@$"Flight number: {flight.Number},
    Arrival time: {flight.ArrivalTime}, Departure Time: {flight.DepartureTime},
    Arrival City: {flight.ArrivalCity}, Departure City: {flight.DepartureCity},
    Terminal: {flight.Terminal}, Status: {Helper.GetEnumDescription(flight.Status)},
    Price: {flight.Price}");
        }
        private static void showFlights(Flight[] flights)
        {
            if (flights == null)
                return;

            if (flights.Length == 0)
            {
                Panel.showError("No flights found");
                return;
            }

            foreach (var flight in flights)
                showFlight(flight);

            Console.WriteLine();
        }
        private static void showPassenger(Passenger passenger)
        {
            if (passenger == null)
                return;

            Console.WriteLine(@$"Fisrtname: {passenger.FirstName}, Secondname: {passenger.SecondName},
    Gender: {passenger.Gender}, Nationality: {passenger.Nationality},
    Birthday: {passenger.Birthday},
    Flight number: {passenger.FlightNumber}, Flight class: {passenger.TicketType},
    Passport: {passenger.Passport}");
        }
        private static void showPassengers(Passenger[] passengers)
        {
            if (passengers == null)
                return;

            if (passengers.Length == 0)
            {
                Panel.showError("No passengers found");
                return;
            }

            foreach (var passenger in passengers)
                showPassenger(passenger);

            Console.WriteLine();
        }

        public void ShowFlights()
        {
            showFlights(this.Flights);
        }
        public void ShowPassengers(Flight flight)
        {
            showPassengers(flight.Passengers);
        }
        public void ShowFlights(FlightSearchPattern searchPattern)
        {
            switch (searchPattern)
            {
                case FlightSearchPattern.ByFlightNumber:
                    Console.Write("Please, enter flight number: ");

                    var flightNumber = default(Guid);
                    var succeed = Guid.TryParse(Console.ReadLine(), out flightNumber);

                    if (!succeed)
                    {
                        Panel.showError("You must enter flight number.");
                        break;
                    }

                    var flight = this.SearchByFlightNumber(flightNumber);

                    if (flight == null)
                    {
                        Panel.showError($"There is no flight with number: {flightNumber}.");
                        break;
                    }

                    Panel.showFlight(flight);
                    break;
                case FlightSearchPattern.ByFlightPrice:
                    Console.Write("Please, enter flight price: ");

                    var flightPrice = default(uint);
                    succeed = uint.TryParse(Console.ReadLine(), out flightPrice);

                    if (!succeed)
                    {
                        Panel.showError("You must enter non-negative number.");
                        break;
                    }

                    var flights = this.SearchByFlightPrice(flightPrice);

                    if (flights == null)
                    {
                        Panel.showError($"There is no flight with price: {flightPrice}.");
                        break;
                    }

                    Panel.showFlights(flights);
                    break;
                case FlightSearchPattern.ByFlightArrivalCity:
                    for (var i = 1; i <= Enum.GetValues(typeof(City)).Length; i++)
                        Console.WriteLine($"{i} = {(City)i}");

                    Console.Write("Please, enter flight arrival city: ");

                    var city = default(City);
                    succeed = Enum.TryParse(Console.ReadLine(), out city);

                    if (!succeed)
                    {
                        Panel.showError("You must enter valid city.");
                        break;
                    }

                    flights = this.SearchByFlightArrivalCity(city);

                    if (flights == null)
                    {
                        Panel.showError($"There is no flight with arrival city: {city}.");
                        break;
                    }

                    Panel.showFlights(flights);
                    break;
                case FlightSearchPattern.ByFlightDepartureCity:
                    for (var i = 1; i <= Enum.GetValues(typeof(City)).Length; i++)
                        Console.WriteLine($"{i} = {(City)i}");

                    Console.Write("Please, enter flight departure city: ");

                    city = default(City);
                    succeed = Enum.TryParse(Console.ReadLine(), out city);

                    if (!succeed)
                    {
                        Panel.showError("You must enter valid city.");
                        break;
                    }

                    flights = this.SearchByFlightDepartureCity(city);

                    if (flights == null)
                    {
                        Panel.showError($"There is no flight with departure city: {city}.");
                        break;
                    }

                    Panel.showFlights(flights);
                    break;
            }
        }
        public void ShowPassengers(PassengerSearchPattern searchPattern)
        {
            switch (searchPattern)
            {
                case PassengerSearchPattern.All:
                    Console.Write("Please, enter flight number: ");

                    var flightNumber = default(Guid);
                    var succeed = Guid.TryParse(Console.ReadLine(), out flightNumber);

                    if (!succeed)
                    {
                        Panel.showError("You must enter flight number.");
                        break;
                    }

                    var flight = this.SearchByFlightNumber(flightNumber);

                    if (flight == null)
                    {
                        Panel.showError($"There is no flight with number: {flightNumber}.");
                        break;
                    }

                    this.ShowPassengers(flight);
                    break;
                case PassengerSearchPattern.ByPassengerFirstname:
                    Console.Write("Please, enter flight number: ");

                    flightNumber = default(Guid);
                    succeed = Guid.TryParse(Console.ReadLine(), out flightNumber);

                    if (!succeed)
                    {
                        Panel.showError("You must enter flight number.");
                        break;
                    }

                    flight = this.SearchByFlightNumber(flightNumber);

                    if (flight == null)
                    {
                        Panel.showError($"There is no flight with number: {flightNumber}.");
                        break;
                    }

                    Console.Write("Please, eneter passenger first name: ");
                    var firstName = Console.ReadLine();

                    var passengers = Panel.SearchByPassengerFirstname(flight, firstName);

                    if (passengers == null)
                    {
                        Panel.showError($"There is no passengers with first name: {firstName}.");
                        break;
                    }

                    Panel.showPassengers(passengers);
                    break;
                case PassengerSearchPattern.ByPassengerSecondname:
                    Console.Write("Please, enter flight number: ");

                    flightNumber = default(Guid);
                    succeed = Guid.TryParse(Console.ReadLine(), out flightNumber);

                    if (!succeed)
                    {
                        Panel.showError("You must enter flight number.");
                        break;
                    }

                    flight = this.SearchByFlightNumber(flightNumber);

                    if (flight == null)
                    {
                        Panel.showError($"There is no flight with number: {flightNumber}.");
                        break;
                    }

                    Console.Write("Please, eneter passenger second name: ");
                    var secondName = Console.ReadLine();

                    passengers = Panel.SearchByPassengerSecondname(flight, secondName);

                    if (passengers == null)
                    {
                        Panel.showError($"There is no passengers with second name: {secondName}.");
                        break;
                    }

                    Panel.showPassengers(passengers);
                    break;
                case PassengerSearchPattern.ByPassengerPassport:
                    Console.Write("Please, enter flight number: ");

                    flightNumber = default(Guid);
                    succeed = Guid.TryParse(Console.ReadLine(), out flightNumber);

                    if (!succeed)
                    {
                        Panel.showError("You must enter flight number.");
                        break;
                    }

                    flight = this.SearchByFlightNumber(flightNumber);

                    if (flight == null)
                    {
                        Panel.showError($"There is no flight with number: {flightNumber}.");
                        break;
                    }

                    Console.Write("Please, eneter passenger passport: ");
                    var passport = Console.ReadLine();

                    var passenger = Panel.SearchByPassengerPassport(flight, passport);

                    if (passenger == null)
                    {
                        Panel.showError($"There is no passengers with passport: {passport}.");
                        break;
                    }

                    Panel.showPassenger(passenger);
                    break;
            }
        }
        private static bool askForFlightInfo(
            ref uint price,
            ref City arrivalCity,
            ref City departureCity,
            ref Terminal terminal,
            ref DateTime arrivalTime,
            ref DateTime departureTime)
        {
            var succeed = false;

            Console.Write("Please, specify flight price: ");
            succeed = uint.TryParse(Console.ReadLine(), out price);
            if (!succeed)
            {
                Panel.showError("You entered wrong price, price must be non-negative integer.");
                return false;
            }

            Console.WriteLine("Please, specify flight arrival city: ");
            foreach (var city in Enum.GetValues(typeof(City)))
                Console.WriteLine(@$"   {(int)(City)city} = {(City)city}");

            Console.Write("> ");
            succeed = City.TryParse(Console.ReadLine(), out arrivalCity);
            if (!succeed)
            {
                Panel.showError("You entered wrong arrival city.");
                return false;
            }
            if ((int)arrivalCity < 1 || (int)arrivalCity > 8)
            {
                Panel.showError("You entered wrong number.");
                return false;
            }

            Console.WriteLine("Please, specify flight departure city: ");
            foreach (var city in Enum.GetValues(typeof(City)))
                Console.WriteLine(@$"   {(int)(City)city} = {(City)city}");

            Console.Write("> ");
            succeed = City.TryParse(Console.ReadLine(), out departureCity);
            if (!succeed)
            {
                Panel.showError("You entered wrong departure city.");
                return false;
            }
            if ((int)departureCity < 1 || (int)departureCity > 8)
            {
                Panel.showError("You entered wrong number.");
                return false;
            }
            if (departureCity == arrivalCity)
            {
                Panel.showError("Departure and arrival city can't be the same city.");
                return false;
            }

            Console.WriteLine("Please, specify flight terminal: ");
            foreach (var currentTerminal in Enum.GetValues(typeof(Terminal)))
                Console.WriteLine(@$"   {(int)(Terminal)currentTerminal} = {(Terminal)currentTerminal}");

            Console.Write("> ");
            succeed = Terminal.TryParse(Console.ReadLine(), out terminal);
            if (!succeed)
            {
                Panel.showError("You entered wrong terminal.");
                return false;
            }
            if ((int)terminal < 1 || (int)terminal > 26)
            {
                Panel.showError("You entered wrong value.");
                return false;
            }

            Console.Write("Please, specify flight arrival time, (in format dd/mm/yy mm:hh): ");
            succeed = DateTime.TryParse(Console.ReadLine(), out arrivalTime);
            if (!succeed)
            {
                Panel.showError("You entered wrong arrival time, time must be in this format dd/mm/yy mm:hh.");
                return false;
            }

            Console.Write("Please, specify flight departure time, (in format dd/mm/yy mm:hh): ");
            succeed = DateTime.TryParse(Console.ReadLine(), out departureTime);
            if (!succeed)
            {
                Panel.showError("You entered wrong departure time, time must be in this format dd/mm/yy mm:hh..");
                return false;
            }

            return true;
        }
        public void AskForFlightInfo()
        {
            var price = default(uint);
            var arrivalCity = default(City);
            var departureCity = default(City);
            var terminal = default(Terminal);
            var arrivalTime = default(DateTime);
            var departureTime = default(DateTime);

            var succeed = Panel.askForFlightInfo(
                ref price,
                ref arrivalCity,
                ref departureCity,
                ref terminal,
                ref arrivalTime,
                ref departureTime
            );

            if (!succeed)
            {
                Panel.showError("Operation failed.");
                return;
            }

            var flight = new Flight(this.Airline, price, arrivalCity, departureCity, terminal, arrivalTime, departureTime);
            this.AddFlight(flight);
        }
        public void AskForEditingFlight()
        {
            var flightNumber = Guid.Empty;
            var price = default(uint);
            var arrivalCity = default(City);
            var departureCity = default(City);
            var terminal = default(Terminal);
            var arrivalTime = default(DateTime);
            var departureTime = default(DateTime);

            Console.Write("Please, enter flight number: ");
            var flightNumberToSearch = Console.ReadLine();
            var succeed = Guid.TryParse(flightNumberToSearch, out flightNumber);
            if (!succeed)
            {
                Panel.showError("Wrong flight number entered.");
                Panel.showError("Operation failed.");
                return;
            }

            var flight = this.SearchByFlightNumber(flightNumber);
            if (flight == null)
            {
                Panel.showError($"There is no flight with number: {flightNumberToSearch}");
                Panel.showError("Operation failed.");
                return;
            }

            succeed = Panel.askForFlightInfo(
                ref price,
                ref arrivalCity,
                ref departureCity,
                ref terminal,
                ref arrivalTime,
                ref departureTime
            );

            if (!succeed)
            {
                Panel.showError("Operation failed.");
                return;
            }

            flight.ChangeInfo(this.Airline, price, arrivalCity, departureCity, terminal, arrivalTime, departureTime);
        }
        public void AskForDeletingFlight()
        {
            var flightNumber = Guid.Empty;

            Console.Write("Please, enter flight number: ");
            var flightNumberToSearch = Console.ReadLine();
            var succeed = Guid.TryParse(flightNumberToSearch, out flightNumber);
            if (!succeed)
            {
                Panel.showError("Wrong flight number entered.");
                Panel.showError("Operation failed.");
                return;
            }

            var flight = this.SearchByFlightNumber(flightNumber);
            if (flight == null)
            {
                Panel.showError($"There is no flight with number: {flightNumberToSearch}");
                Panel.showError("Operation failed.");
                return;
            }

            this.RemoveFlight(flight);
        }

        private static bool askForPassengerInfo
        (
            ref string firstName,
            ref string secondName,
            ref string nationality,
            ref string passport,
            ref DateTime birthday,
            ref Gender gender,
            ref TicketType ticket
        )
        {
            var succeed = false;

            Console.Write("Please, enter passenger first name: ");
            firstName = Console.ReadLine();

            Console.Write("Please, enter passenger second name: ");
            secondName = Console.ReadLine();

            Console.Write("Please, enter passenger nationality: ");
            nationality = Console.ReadLine();

            Console.Write("Please, enter passenger passport: ");
            passport = Console.ReadLine();

            Console.Write("Please, specify passenger birthday, (in format dd/mm/yy mm:hh): ");
            succeed = DateTime.TryParse(Console.ReadLine(), out birthday);
            if (!succeed)
            {
                Panel.showError("You entered wrong date, date must be in this format dd/mm/yy mm:hh.");
                return false;
            }

            Console.WriteLine("Please, enter passenger gender: ");
            foreach (var currentGender in Enum.GetValues(typeof(Gender)))
                Console.WriteLine(@$"   {(int)(Gender)currentGender} = {(Gender)currentGender}");

            Console.Write("> ");
            succeed = Gender.TryParse(Console.ReadLine(), out gender);
            if (!succeed)
            {
                Panel.showError("You entered wrong gender.");
                return false;
            }
            if ((int)gender > 2 || (int)gender < 1)
            {
                Panel.showError("You entered wrong number.");
                return false;
            }

            Console.WriteLine("Please, specify ticket type: ");
            foreach (var ticketType in Enum.GetValues(typeof(TicketType)))
                Console.WriteLine(@$"   {(int)(TicketType)ticketType} = {(TicketType)ticketType}");

            Console.Write("> ");
            succeed = TicketType.TryParse(Console.ReadLine(), out ticket);
            if (!succeed)
            {
                Panel.showError("You entered wrong ticket type..");
                return false;
            }
            if ((int)ticket > 2 || (int)ticket < 1)
            {
                Panel.showError("You entered wrong number.");
                return false;
            }

            return true;
        }
        public void AskForPassengerInfo()
        {
            var flightNumber = Guid.Empty;
            var firstName = string.Empty;
            var secondName = string.Empty;
            var nationality = string.Empty;
            var passport = string.Empty;
            var birthday = default(DateTime);
            var gender = default(Gender);
            var ticket = default(TicketType);

            Console.Write("Please, enter flight number: ");
            var flightNumberToSearch = Console.ReadLine();
            var succeed = Guid.TryParse(flightNumberToSearch, out flightNumber);
            if (!succeed)
            {
                Panel.showError("Wrong flight number entered.");
                Panel.showError("Operation failed.");
                return;
            }

            var flight = this.SearchByFlightNumber(flightNumber);
            if (flight == null)
            {
                Panel.showError($"There is no flight with number: {flightNumberToSearch}");
                Panel.showError("Operation failed.");
                return;
            }

            succeed = Panel.askForPassengerInfo(
                ref firstName,
                ref secondName,
                ref nationality,
                ref passport,
                ref birthday,
                ref gender,
                ref ticket
            );

            if (!succeed)
            {
                Panel.showError("Operation failed.");
                return;
            }

            var passenger = new Passenger(firstName, secondName, nationality, passport, birthday, gender);
            passenger.BuyTicket(flight, ticket);
            flight.AddPassenger(passenger);
        }
        public void AskForDeletingPassenger()
        {
            var flightNumber = Guid.Empty;

            Console.Write("Please, enter flight number: ");
            var flightNumberToSearch = Console.ReadLine();
            var succeed = Guid.TryParse(flightNumberToSearch, out flightNumber);
            if (!succeed)
            {
                Panel.showError("Wrong flight number entered.");
                Panel.showError("Operation failed.");
                return;
            }

            var flight = this.SearchByFlightNumber(flightNumber);
            if (flight == null)
            {
                Panel.showError($"There is no flight with number: {flightNumberToSearch}");
                Panel.showError("Operation failed.");
                return;
            }

            Console.Write("Please, enter passenger passport: ");
            var passportToSearch = Console.ReadLine();
            var passenger = Panel.SearchByPassengerPassport(flight, passportToSearch);
            if (passenger == null)
            {
                Panel.showError("Wrong passenger passport entered.");
                Panel.showError("Operation failed.");
                return;
            }

            flight.RemovePassenger(passenger);
        }

        public Flight SearchByFlightNumber(Guid flightNumber)
        {
            foreach (var flight in this.Flights)
                if (flight.Number == flightNumber)
                    return flight;

            return null;
        }
        public Flight[] SearchByFlightPrice(uint flightPrice)
        {
            var flights = new Flight[0];
            var found = false;

            foreach (var flight in this.Flights)
                if (flight.Price == flightPrice)
                {
                    flights = Helper.ResizeArray(flights, flights.Length + 1);

                    flights[flights.Length - 1] = flight;
                    found = true;
                }

            return found ? flights : null;
        }
        public Flight[] SearchByFlightArrivalCity(City city)
        {
            var flights = new Flight[0];
            var found = false;

            foreach (var flight in this.Flights)
                if (flight.ArrivalCity == city)
                {
                    flights = Helper.ResizeArray(flights, flights.Length + 1);
                    flights[flights.Length - 1] = flight;
                    found = true;
                }

            return found ? flights : null;
        }
        public Flight[] SearchByFlightDepartureCity(City city)
        {
            var flights = new Flight[0];
            var found = false;

            foreach (var flight in this.Flights)
                if (flight.DepartureCity == city)
                {
                    flights = Helper.ResizeArray(flights, flights.Length + 1);
                    flights[flights.Length - 1] = flight;
                    found = true;
                }

            return found ? flights : null;
        }
        public static Passenger[] SearchByPassengerFirstname(Flight flight, string firstName)
        {
            var passengers = new Passenger[0];
            var found = false;

            foreach (var passenger in flight.Passengers)
                if (passenger.FirstName == firstName)
                {
                    passengers = Helper.ResizeArray(passengers, passengers.Length + 1);
                    passengers[passengers.Length - 1] = passenger;
                    found = true;
                }

            return found ? passengers : null;
        }
        public static Passenger[] SearchByPassengerSecondname(Flight flight, string secondName)
        {
            var passengers = new Passenger[0];
            var found = false;

            foreach (var passenger in flight.Passengers)
                if (passenger.SecondName == secondName)
                {
                    passengers = Helper.ResizeArray(passengers, passengers.Length + 1);
                    passengers[passengers.Length - 1] = passenger;
                    found = true;
                }

            return found ? passengers : null;
        }
        public static Passenger SearchByPassengerPassport(Flight flight, string passport)
        {
            foreach (var passenger in flight.Passengers)
                if (passenger.Passport == passport)
                    return passenger;

            return null;
        }
    }
}
