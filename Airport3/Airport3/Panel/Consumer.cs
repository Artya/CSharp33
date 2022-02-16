using System;
using System.Collections.Generic;
using System.Linq;
using Airport3.Enums;
using Airport3.Interfaces;
using Spectre.Console;

namespace Airport3.Panel
{
    public class Consumer : IConsumer, IObserver<Message>
    {
        private IProvider provider;

        public readonly Airline Airline;

        public Consumer(Airline airline)
        {
            this.Airline = airline;
        }

        public void SubscribeOn(IProvider provider)
        {
            if (this.provider == null)
                this.provider = provider;

            provider.Subscribe(this);
        }

        public void OnCompleted()
        {
            Console.WriteLine("Exiting...");
        }
        public void OnError(Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error.Message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void OnNext(Message message)
        {
            switch (message.MessageType)
            {
                case MessageType.Start:
                    this.mainLoop();
                    break;
                case MessageType.Succeed:
                    this.onSucceed();
                    break;
                case MessageType.ShowFLight:
                    this.onShowFlight(message.Flight);
                    break;
                case MessageType.ShowPassenger:
                    this.onShowPassenger(message.Passenger);
                    break;
                default:
                    break;
            }
        }

        private void helloMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{this.Airline.Name} panel");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void onSucceed()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Operation succeed!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private void onShowFlight(Flight flight)
        {
            Console.WriteLine(@$"Flight number: {flight.Number},
    Arrival time: {flight.ArrivalTime}, Departure Time: {flight.DepartureTime},
    Arrival City: {flight.ArrivalCity}, Departure City: {flight.DepartureCity},
    Terminal: {flight.Terminal}, Status: {Helper.GetEnumDescription(flight.Status, true)},
    Price: {flight.Price}");
        }
        private void onShowPassenger(Passenger passenger)
        {
            Console.WriteLine(@$"Fisrtname: {passenger.FirstName}, Secondname: {passenger.SecondName},
    Gender: {passenger.Gender}, Nationality: {passenger.Nationality},
    Birthday: {passenger.Birthday},
    Flight number: {passenger.FlightNumber}, Flight class: {passenger.TicketType},
    Passport: {passenger.Passport}");
        }
        private void mainLoop()
        {
            var exit = false;
            while (!exit)
            {
                var operations = new List<string>();
                foreach (var item in Enum.GetValues(typeof(OperationType)))
                    operations.Add(Helper.GetEnumDescription((OperationType)item, true));

                this.helloMessage();
                var operation = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                        .Title("Please, choose the operation:")
                        .PageSize(Enum.GetValues(typeof(OperationType)).Length)
                        .AddChoices(operations)
                );

                switch (Helper.ParseDescriptionToEnum<OperationType>(operation, true))
                {
                    case OperationType.ShowAllFlights:
                        this.provider.Recieve(
                            new Message(MessageType.QueryAllFlights));
                        wait();
                        break;
                    case OperationType.ShowAllPassengersForSpecificFlight:
                        this.provider.Recieve(
                            new Message(MessageType.QueryAllPassengers, this.askFlightNumber()));
                        wait();
                        break;
                    case OperationType.SearchByFlightNumber:
                        this.provider.Recieve(
                            new Message(MessageType.QueryFlightByFlightNumber, this.askFlightNumber()));
                        wait();
                        break;
                    case OperationType.SearchByFlightPrice:
                        this.provider.Recieve(
                            new Message(MessageType.QueryFlightsByFlightPrice, this.askFlightPrice()));
                        wait();
                        break;
                    case OperationType.SearchByFlightArrivalCity:
                        this.provider.Recieve(
                            new Message(MessageType.QueryFlightsByFlightArrivalCity, this.askFlightArrivalCity()));
                        wait();
                        break;
                    case OperationType.SearchByFlightDepartureCity:
                        this.provider.Recieve(
                            new Message(MessageType.QueryFlightsByFlightDepartureCity, this.askFlightDepartureCity()));
                        wait();
                        break;
                    case OperationType.SearchByPassengerFirstName:
                        this.provider.Recieve(
                            new Message(MessageType.QueryPassengersByFirtstName, this.askFlightNumber(), this.askPassengerFirstName()));
                        wait();
                        break;
                    case OperationType.SearchByPassengerSecondName:
                        this.provider.Recieve(
                            new Message(MessageType.QueryPassengersBySecondName, this.askFlightNumber(), this.askPassengerSecondName()));
                        wait();
                        break;
                    case OperationType.SearchByPassengerPassport:
                        this.provider.Recieve(
                            new Message(MessageType.QueryPassengerByPassport, this.askFlightNumber(), this.askPassengerPassport()));
                        wait();
                        break;
                    case OperationType.AddFlight:
                        this.provider.Recieve(
                            new Message(MessageType.AddFlight, this.askFlightInfo(true)));
                        wait();
                        break;
                    case OperationType.EditFlight:
                        this.provider.Recieve(
                            new Message(MessageType.EditFlight, this.askFlightInfo(false)));
                        wait();
                        break;
                    case OperationType.DeleteFlight:
                        this.provider.Recieve(
                            new Message(MessageType.DeleteFlight, this.askFlightNumber()));
                        wait();
                        break;
                    case OperationType.AddPassenger:
                        this.provider.Recieve(
                            new Message(MessageType.AddPassenger, this.askPassengerInfo()));
                        wait();
                        break;
                    case OperationType.DeletePassenger:
                        this.provider.Recieve(
                            new Message(MessageType.DeletePassenger, this.askPassengerToDelete()));
                        wait();
                        break;
                    case OperationType.Exit:
                        exit = true;
                        break;
                }
            }
        }

        private string askFlightNumber()
        {
            var flightNumber = default(Guid);
            var succeed = false;

            while (!succeed)
            {
                Console.Write("Please, enter flight number: ");
                succeed = Guid.TryParse(Console.ReadLine(), out flightNumber);

                if (!succeed)
                    this.OnError(new Exception("Input value was in incorrect format."));
            }

            return flightNumber.ToString();
        }
        private string askFlightPrice()
        {
            var flightPrice = default(uint);
            var succeed = false;

            while (!succeed)
            {
                Console.Write("Please, enter flight price: ");
                succeed = uint.TryParse(Console.ReadLine(), out flightPrice);

                if (!succeed)
                    this.OnError(new Exception("Input wasn't a valid number, must be non-negative number."));
            }

            return flightPrice.ToString();
        }
        private string askFlightCity(string messageToDisplay)
        {
            var cities = Helper.GetEnumValues<City>();

            var city = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                        .Title(messageToDisplay)
                        .PageSize(Enum.GetValues(typeof(City)).Length)
                        .AddChoices(cities)
                );

            return Helper.ParseDescriptionToEnum<City>(city, false).ToString();
        }
        private string askFlightArrivalCity()
        {
            return this.askFlightCity("Please, choose the arrival city:");
        }
        private string askFlightDepartureCity()
        {
            return this.askFlightCity("Please, choose the departure city:");
        }
        private string askPassengerFirstName()
        {
            Console.Write("Please, enter passenger first name: ");
            return Console.ReadLine();
        }
        private string askPassengerSecondName()
        {
            Console.Write("Please, enter passenger second name: ");
            return Console.ReadLine();
        }
        private string askPassengerPassport()
        {
            Console.Write("Please, enter passenger passport: ");
            return Console.ReadLine();
        }
        private Flight askFlightInfo(bool createNew)
        {
            var succeed = false;
            var flight = default(Flight);
            var flightNumber = default(string);
            var price = default(uint);
            var arrivalCity = default(City);
            var departureCity = default(City);
            var terminal = default(Terminal);
            var arrivalTime = default(DateTime);
            var departureTime = default(DateTime);

            if (!createNew)
            {
                do
                {
                    flightNumber = this.askFlightNumber();
                    Console.Clear();
                    this.helloMessage();

                    var flightExists = this.provider.Flights.Exists(flight => flight.Number.ToString() == flightNumber);

                    if (!flightExists)
                    {
                        this.OnError(new Exception($"There is no flight with number: {flightNumber}"));
                        wait();
                        continue;
                    }

                    succeed = true;
                }
                while (!succeed);
            }

            price = uint.Parse(this.askFlightPrice());
            Console.Clear();
            this.helloMessage();

            arrivalCity = (City)Enum.Parse(typeof(City), this.askFlightArrivalCity());
            Console.Clear();
            this.helloMessage();

            do
            {
                departureCity = (City)Enum.Parse(typeof(City), this.askFlightDepartureCity());

                if (departureCity == arrivalCity)
                {
                    this.OnError(new Exception("Arrival and departure cities can't be the same"));
                    wait();
                    this.helloMessage();
                    continue;
                }

                succeed = true;
            }
            while (!succeed);

            var terminals = Helper.GetEnumValues<Terminal>();

            terminal = Helper.ParseDescriptionToEnum<Terminal>(AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                        .Title("Please, choose terminal:")
                        .PageSize(Enum.GetValues(typeof(Terminal)).Length)
                        .AddChoices(terminals)
                ),
                false
            );
            Console.Clear();

            do
            {
                this.helloMessage();

                Console.Write("Please, enter arrival time, in format: (dd/mm/yy hh:mm) ");
                succeed = DateTime.TryParse(Console.ReadLine(), out arrivalTime);

                if (!succeed)
                    this.OnError(new Exception("Arrival time must be in format: dd/yy/mm hh:mm"));

                Console.Clear();
            }
            while (!succeed);

            do
            {
                this.helloMessage();

                Console.Write("Please, enter departure time, in format: (dd/mm/yy hh:mm) ");
                succeed = DateTime.TryParse(Console.ReadLine(), out departureTime);

                if (!succeed)
                {
                    this.OnError(new Exception("Departure time must be in format: dd/yy/mm hh:mm"));
                    wait();
                    continue;
                }

                if (arrivalTime <= departureTime)
                {
                    this.OnError(new ApplicationException("Arrival time can't be later or equal to departure time"));
                    succeed = false;
                    wait();
                    continue;
                }
            }
            while (!succeed);

            if (createNew)
                return new Flight(this.Airline, price, arrivalCity, departureCity, terminal, arrivalTime, departureTime);

            flight = this.provider.Flights
                .Single(flight => flight.Number.ToString() == flightNumber);

            flight.ChangeInfo(price, arrivalCity, departureCity, terminal, arrivalTime, departureTime);
            return flight;
        }
        private Passenger askPassengerInfo()
        {
            var succeed = false;
            var firstName = default(string);
            var secondName = default(string);
            var nationality = default(string);
            var birthday = default(DateTime);
            var gender = default(Gender);
            var flight = default(Flight);
            var passport = default(string);
            var ticketType = default(TicketType);

            do
            {
                var flightNumber = this.askFlightNumber();
                flight = this.provider.Flights
                    .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

                if (flight == null)
                {
                    this.OnError(new Exception($"There is no flight with number: {flightNumber}"));
                    wait();
                    this.helloMessage();
                    continue;
                }

                succeed = true;
            }
            while (!succeed);

            firstName = this.askPassengerFirstName();
            Console.Clear();

            this.helloMessage();
            secondName = this.askPassengerSecondName();
            Console.Clear();

            this.helloMessage();
            Console.Write("Please, enter passenger nationality: ");
            nationality = Console.ReadLine();
            Console.Clear();

            do
            {
                this.helloMessage();
                Console.Write("Please, enter passenger birthday, in format (dd/yy/mm) ");
                succeed = DateTime.TryParse(Console.ReadLine(), out birthday);

                if (!succeed)
                {
                    this.OnError(new Exception("Wrong date format."));
                    wait();
                    continue;
                }

                if (DateTime.Now <= birthday)
                {
                    this.OnError(new Exception("Birthday can't be this day or in future."));
                    wait();
                    succeed = false;
                    continue;
                }
            }
            while (!succeed);

            Console.Clear();
            this.helloMessage();

            var genders = Helper.GetEnumValues<Gender>();

            var choosenGender = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                        .Title("Please, choose passenger gender:")
                        .PageSize(Enum.GetValues(typeof(Gender)).Length + 1)
                        .AddChoices(genders)
                );

            gender = Helper.ParseDescriptionToEnum<Gender>(choosenGender, false);
            Console.Clear();

            this.helloMessage();
            passport = this.askPassengerPassport();
            Console.Clear();

            this.helloMessage();
            var ticketTypes = Helper.GetEnumValues<TicketType>();

            var choosenTicketType = AnsiConsole.Prompt
                (
                    new SelectionPrompt<string>()
                        .Title("Please, choose passenger gender:")
                        .PageSize(Enum.GetValues(typeof(TicketType)).Length + 1)
                        .AddChoices(ticketTypes)
                );

            ticketType = Helper.ParseDescriptionToEnum<TicketType>(choosenTicketType, false);

            var passenger = new Passenger(firstName, secondName, nationality, passport, birthday, gender);
            passenger.BuyTicket(flight, ticketType);

            return passenger;
        }
        private Passenger askPassengerToDelete()
        {
            var succeed = false;
            var flight = default(Flight);
            var passenger = default(Passenger);

            do
            {
                var flightNumber = this.askFlightNumber();
                flight = this.provider.Flights
                    .SingleOrDefault(flight => flight.Number.ToString() == flightNumber);

                if (flight == null)
                {
                    this.OnError(new Exception($"There is no flight with number: {flightNumber}"));
                    wait();
                    this.helloMessage();
                    continue;
                }

                succeed = true;
            }
            while (!succeed);

            Console.Clear();

            do
            {
                this.helloMessage();

                var passengerPassport = this.askPassengerPassport();
                passenger = flight.Passengers
                    .FirstOrDefault(passenger => passenger.Passport == passengerPassport);

                if (passenger == null)
                {
                    this.OnError(new Exception($"There is no passenger with passport: {passengerPassport}"));
                    wait();
                    succeed = false;
                    continue;
                }

                succeed = true;
            }
            while (!succeed);

            return passenger;
        }

        private static void wait()
        {
            Console.ReadKey();
            Console.Clear();
        }
    }
}
