using System;
using System.Globalization;

namespace Airport1
{
    public static class Program
    {
        public enum FlightsArrayLevel
        {
            FlightType,
            DateTime,
            CityPort,
            Airline,
            Terminal,
            FlightStatus,
            Gate
        }

        public static object[,] FlightsArray;
        public static bool EmergencyIsSet;
        public static EmergencyType EmergencyStatus;

        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);

            while (true)
            {
                if (EmergencyIsSet)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Evacuation, reason: {EmergencyStatus}!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                Console.WriteLine(@$"Please choose operation.
{(int)OperationType.AddFlight} - Add flight
{(int)OperationType.ShowArrival} - Arrivals
{(int)OperationType.ShowDepartures} - Departures
{(int)OperationType.EditInformation} - Edit inforamtion
{(int)OperationType.SearchByFlightNumber} - Search by flight number
{(int)OperationType.SearchByTime} - Search by specified time in airport
{(int)OperationType.SearchNearest} - Search nearest
{(int)OperationType.SearchByPort} - Search by port
{(int)OperationType.SetEmergencyStatus} - Set emergency status
{(int)OperationType.Exit} - Exit");

                var operationType = (OperationType)int.Parse(Console.ReadLine());
                switch (operationType)
                {
                    case OperationType.AddFlight: AddFlight(); break;
                    case OperationType.ShowArrival: ShowArrivals(); break;
                    case OperationType.ShowDepartures: ShowDepartures(); break;
                    case OperationType.EditInformation: EditInformation(); break;
                    case OperationType.SearchByFlightNumber: SearchByFlightNumber(); break;
                    case OperationType.SearchByTime: SearchByTime(); break;
                    case OperationType.SearchNearest: SearchNearest(); break;
                    case OperationType.SearchByPort: SearchByPort(); break;
                    case OperationType.SetEmergencyStatus: SetEmergencyStatus(); break;
                    case OperationType.Exit: return;
                    default: Console.WriteLine($"Program doesn't has operation type id {(int)operationType}"); break;
                }
            }
        }

        public static void AddFlight()
        {
            Console.WriteLine();
            Console.WriteLine(@$"Enter flight type:
{(int)FlightType.Arrival} - Arrival
{(int)FlightType.Departure} - Departure");

            var flightType = (FlightType)int.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter flight date/time in format dd/mm/yy hh:mm");
            var dateTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter flight city/port");
            var cityPort = Console.ReadLine();

            Console.WriteLine(@$"Enter airline");
            var airline = Console.ReadLine();

            Console.WriteLine(@$"Enter terminal");
            Console.WriteLine(@"    Terminals:");
            const int terminalCount = 26;
            for (int i = 1; i <= terminalCount; i++)
            {
                var terminal = ((TerminalType)i).ToString();
                Console.WriteLine(@$"    {i} = {terminal}");
            }
            var terminalType = (TerminalType)int.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter flight status");
            Console.WriteLine(@"    Flight Statuses:");
            const int flightStatusesCount = 9;
            for (int i = 1; i <= flightStatusesCount; i++)
            {
                var flightStatus = ((FlightStatusType)i).ToString();
                Console.WriteLine(@$"    {i} = {flightStatus}");
            }
            var flightStatusType = (FlightStatusType)int.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter gate:");
            var gate = Console.ReadLine();

            AppendFlight(flightType, dateTime, cityPort, airline, terminalType, flightStatusType, gate);
            Console.WriteLine();
        }

        public static void ShowArrivals()
        {
            var empty = CheckFlightsArrayForEmptiness();
            if (empty)
                return;

            Console.WriteLine();
            Console.WriteLine("Arrival flights:");

            var found = false;
            for (var i = 0; i < FlightsArray.GetLength(1); i++)
            {
                var flightType = (FlightType)FlightsArray[0, i];
                if (flightType == FlightType.Arrival)
                {
                    found = true;
                    PrintFlight(i);
                }
            }

            if (!found)
            {
                Console.WriteLine("No arrival flights were found");
            }
            Console.WriteLine();
        }

        public static void ShowDepartures()
        {
            var empty = CheckFlightsArrayForEmptiness();
            if (empty)
                return;

            Console.WriteLine();
            Console.WriteLine("Departure flights:");

            var found = false;
            for (var i = 0; i < FlightsArray.GetLength(1); i++)
            {
                var flightType = (FlightType)FlightsArray[0, i];
                if (flightType == FlightType.Departure)
                {
                    found = true;
                    PrintFlight(i);
                }
            }

            if (!found)
            {
                Console.WriteLine("No departure flights were found");
            }
            Console.WriteLine();
        }

        public static void EditInformation()
        {
            var empty = CheckFlightsArrayForEmptiness();
            if (empty)
                return;

            Console.WriteLine();
            Console.WriteLine("Enter flight number to edit:");
            var flightNumber = int.Parse(Console.ReadLine());

            var flightsCount = FlightsArray.GetLength(1);
            if (flightNumber < 0 || flightNumber > flightsCount)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There is no flight with number {flightNumber}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                return;
            }

            Console.WriteLine(@$"Enter flight type:
{(int)FlightType.Arrival} - Arrival
{(int)FlightType.Departure} - Departure");
            var flightType = (FlightType)int.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter flight date/time in format dd/mm/yy hh:mm");
            var dateTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter flight city/port");
            var cityPort = Console.ReadLine();

            Console.WriteLine(@$"Enter airline");
            var airline = Console.ReadLine();

            Console.WriteLine(@$"Enter terminal");
            Console.WriteLine(@"    Terminals:");
            const int terminalCount = 26;
            for (int i = 1; i <= terminalCount; i++)
            {
                var terminal = ((TerminalType)i).ToString();
                Console.WriteLine(@$"    {i} = {terminal}");
            }
            var terminalType = (TerminalType)int.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter flight status");
            Console.WriteLine(@"    Flight Statuses:");
            const int flightStatusesCount = 9;
            for (int i = 1; i <= flightStatusesCount; i++)
            {
                var flightStatus = ((FlightStatusType)i).ToString();
                Console.WriteLine(@$"    {i} = {flightStatus}");
            }
            var flightStatusType = (FlightStatusType)int.Parse(Console.ReadLine());

            Console.WriteLine(@$"Enter gate:");
            var gate = Console.ReadLine();

            FlightsArray[(int)FlightsArrayLevel.FlightType, flightNumber - 1] = flightType;
            FlightsArray[(int)FlightsArrayLevel.DateTime, flightNumber - 1] = dateTime;
            FlightsArray[(int)FlightsArrayLevel.CityPort, flightNumber - 1] = cityPort;
            FlightsArray[(int)FlightsArrayLevel.Airline, flightNumber - 1] = airline;
            FlightsArray[(int)FlightsArrayLevel.Terminal, flightNumber - 1] = terminalType;
            FlightsArray[(int)FlightsArrayLevel.FlightStatus, flightNumber - 1] = flightStatusType;
            FlightsArray[(int)FlightsArrayLevel.Gate, flightNumber - 1] = gate;

            Console.WriteLine("Successfully edited information!");
            Console.WriteLine();
        }

        public static void SearchByFlightNumber()
        {
            var empty = CheckFlightsArrayForEmptiness();
            if (empty)
                return;

            Console.WriteLine();
            Console.WriteLine("Enter flight number:");
            var flightNumber = int.Parse(Console.ReadLine());

            var flightsCount = FlightsArray.GetLength(1);
            if (flightNumber < 0 || flightNumber > flightsCount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"There is no flight with number {flightNumber}");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            PrintFlight(flightNumber - 1);
            Console.WriteLine();
        }

        public static void SearchByTime()
        {
            var empty = CheckFlightsArrayForEmptiness();
            if (empty)
                return;

            Console.WriteLine();
            Console.WriteLine("Enter time to find flight in specified format dd/mm/yy hh:mm");
            var time = DateTime.Parse(Console.ReadLine());

            var found = false;
            var flightsCount = FlightsArray.GetLength(1);
            for (int i = 0; i < flightsCount; i++)
            {
                var flightTime = (DateTime)FlightsArray[1, i];
                if (DateTime.Compare(flightTime, time) == 0)
                {
                    found = true;
                    PrintFlight(i);
                }
            }

            if (!found)
            {
                Console.WriteLine($"There is no flights with time {time}");
            }
            Console.WriteLine();
        }

        public static void SearchNearest()
        {
            var empty = CheckFlightsArrayForEmptiness();
            if (empty)
                return;

            Console.WriteLine();
            Console.WriteLine("Enter port to find");
            var port = Console.ReadLine();

            Console.WriteLine("Enter time in the specified format dd/mm/yy hh:mm");
            var time = DateTime.Parse(Console.ReadLine());

            var found = false;
            var flightsCount = FlightsArray.GetLength(1);
            for (int i = 0; i < flightsCount; i++)
            {
                var flightTime = (DateTime)FlightsArray[1, i];
                var flightPort = (string)FlightsArray[2, i];

                if (DateTime.Compare(flightTime, time) > 0 && flightPort == port)
                {
                    var differenceInTime = flightTime.Subtract(time).TotalHours;
                    if (differenceInTime <= 1)
                    {
                        found = true;
                        PrintFlight(i);
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine($"There is no flight nearest one hour in {time} time");
            }
            Console.WriteLine();
        }

        public static void SearchByPort()
        {
            var empty = CheckFlightsArrayForEmptiness();
            if (empty)
                return;

            Console.WriteLine();
            Console.WriteLine("Enter port to search");
            var port = Console.ReadLine();

            var found = false;
            var flightsCount = FlightsArray.GetLength(1);
            for (int i = 0; i < flightsCount; i++)
            {
                var flightPort = (string)FlightsArray[2, i];
                if (flightPort == port)
                {
                    found = true;
                    PrintFlight(i);
                }
            }

            if (!found)
            {
                Console.WriteLine($"There is no flight with port: {port}");
            }
            Console.WriteLine();
        }

        public static void SetEmergencyStatus()
        {
            Console.WriteLine("Enter emergency status:");
            Console.WriteLine(@$"    {(int)EmergencyType.Fire} = {EmergencyType.Fire.ToString()}
    {(int)EmergencyType.Evacuation} = {EmergencyType.Evacuation.ToString()}
    {(int)EmergencyType.Mined} = {EmergencyType.Mined.ToString()}");

            EmergencyIsSet = true;
            EmergencyStatus = (EmergencyType)int.Parse(Console.ReadLine());
        }

        public static void PrintFlight(int index)
        {
            Console.WriteLine($@"Flight at {FlightsArray[(int)FlightsArrayLevel.DateTime, index]},
    flight type: {FlightsArray[(int)FlightsArrayLevel.FlightType, index]},
    flight number: {index + 1},
    flight city/port: {FlightsArray[(int)FlightsArrayLevel.CityPort, index]},
    flight airline: {FlightsArray[(int)FlightsArrayLevel.Airline, index]},
    flight terminal: {FlightsArray[(int)FlightsArrayLevel.Terminal, index]},
    flight status: {FlightsArray[(int)FlightsArrayLevel.FlightStatus, index]},
    flight gate: {FlightsArray[(int)FlightsArrayLevel.Gate, index]}");
        }

        public static bool CheckFlightsArrayForEmptiness()
        {
            if (FlightsArray == null)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No flights");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                return true;
            }

            return false;
        }

        public static void AppendFlight(
           FlightType flightType,
           DateTime dateTime,
           string cityPort,
           string airline,
           TerminalType terminalType,
           FlightStatusType flightStatusType,
           string gate
           )
        {
            if (FlightsArray == null)
            {
                FlightsArray = new object[7, 1];

                FlightsArray[(int)FlightsArrayLevel.FlightType, 0] = flightType;
                FlightsArray[(int)FlightsArrayLevel.DateTime, 0] = dateTime;
                FlightsArray[(int)FlightsArrayLevel.CityPort, 0] = cityPort;
                FlightsArray[(int)FlightsArrayLevel.Airline, 0] = airline;
                FlightsArray[(int)FlightsArrayLevel.Terminal, 0] = terminalType;
                FlightsArray[(int)FlightsArrayLevel.FlightStatus, 0] = flightStatusType;
                FlightsArray[(int)FlightsArrayLevel.Gate, 0] = gate;
                return;
            }

            var length = FlightsArray.GetLength(1);
            var newFlightsArray = new object[7, length + 1];

            Copy2DArray(FlightsArray, newFlightsArray);

            newFlightsArray[(int)FlightsArrayLevel.FlightType, length] = flightType;
            newFlightsArray[(int)FlightsArrayLevel.DateTime, length] = dateTime;
            newFlightsArray[(int)FlightsArrayLevel.CityPort, length] = cityPort;
            newFlightsArray[(int)FlightsArrayLevel.Airline, length] = airline;
            newFlightsArray[(int)FlightsArrayLevel.Terminal, length] = terminalType;
            newFlightsArray[(int)FlightsArrayLevel.FlightStatus, length] = flightStatusType;
            newFlightsArray[(int)FlightsArrayLevel.Gate, length] = gate;

            FlightsArray = newFlightsArray;
        }

        public static void Copy2DArray(object[,] sourceArray, object[,] destinationArray)
        {
            for (var i = 0; i < sourceArray.GetLength(1); i++)
            {
                for (var j = 0; j < sourceArray.GetLength(0); j++)
                {
                    destinationArray[j, i] = sourceArray[j, i];
                }
            }
        }
    }
}
