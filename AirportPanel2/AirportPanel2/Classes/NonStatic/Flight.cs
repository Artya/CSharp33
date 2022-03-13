using System;

namespace AirportPanel2
{
    public class Flight
    {
        public Airline Owner { get; }
        public string FlightNumber { get; }
        public DateTime DepartureDateTime { get; private set; }
        public DateTime ArrivalDateTime { get; private set; }
        public Airport DeparturePort { get; }
        public Airport ArrivalPort { get; }
        public FlightStatuses FlightStatus { get; private set; }
        public Terminals DepartureTerminal { get; private set; }
        public Terminals ArrivalTerminal { get; private set; }
        public Gates DepartureGate { get; private set; }
        public Gates ArrivalGate { get; private set; }

        public Flight(Airline airline,
                       string flightNumber,
                       DateTime departureDateTime,
                       DateTime arrivalDateTime,
                       Airport departurePort,
                       Airport arrivalPort,
                       FlightStatuses flightStatus,
                       Terminals departureTerminal,
                       Terminals arrivalTerminal,
                       Gates departureGate,
                       Gates arrivalGate)
        {
            this.Owner = airline;
            this.FlightNumber = flightNumber;
            this.DepartureDateTime = departureDateTime;
            this.ArrivalDateTime = arrivalDateTime;
            this.DeparturePort = departurePort;
            this.ArrivalPort = arrivalPort;
            this.FlightStatus = flightStatus;
            this.DepartureTerminal = departureTerminal;
            this.ArrivalTerminal = arrivalTerminal;
            this.DepartureGate = departureGate;
            this.ArrivalGate = arrivalGate;
        }

        public static Flight GetNewFlightFromUser(Airline owner)
        {
            if (owner == null)
                throw new NullReferenceException("Airline can`t be null");

            var flightNumber = UserInteraction.GetString("Enter flight nuber:");
            var departureDateTime = UserInteraction.GetDate("Enter departure date and time in format dd/mm/yy hh:mm");
            var arrivalDateTime = (DateTime)default;
            var minArrivalDateTime = departureDateTime.AddHours(1);

            do
            {
                arrivalDateTime = UserInteraction.GetDate($"Enter arrival date and time in format dd/mm/yy hh:mm (Not less than {minArrivalDateTime})");
            } while (arrivalDateTime < minArrivalDateTime);

            var departurePort = (Airport)UserInteraction.ChooseElemementFromArray(owner.AvaliableAirports, "Choose departure airport");
            var departureTerminal = (Terminals)UserInteraction.ChooseElemementFromArray(departurePort.AvaliableTerminals, "Choose departure terminal");
            var departureGate = (Gates)UserInteraction.ChooseElemementFromArray(departureTerminal.AvaliableGates, "Choose departure Gate");
            var arrivalPort = (Airport)UserInteraction.ChooseElemementFromArray(owner.AvaliableAirports, "Choose arrival airport", Array.IndexOf(owner.AvaliableAirports, departurePort));
            var arrivalTerminal = (Terminals)UserInteraction.ChooseElemementFromArray(arrivalPort.AvaliableTerminals, "Choose arrival terminal");
            var arrivalGate = (Gates)UserInteraction.ChooseElemementFromArray(arrivalTerminal.AvaliableGates, "Choose arrival Gate");
            var flightStatus = (FlightStatuses)UserInteraction.ChooseEnumValue("Choose flight status", typeof(FlightStatuses));

            return new Flight(owner,
                flightNumber,
                departureDateTime,
                arrivalDateTime,
                departurePort,
                arrivalPort,
                flightStatus,
                departureTerminal,
                arrivalTerminal,
                departureGate,
                arrivalGate);
        }

        public static Flight CreateNewRandomFlight(Airline owner)
        {
            if (owner == null)
                throw new NullReferenceException("Airline can`t be null");

            var rand = new Random();
            var now = DateTime.Now;

            var flightNumber = rand.Next(11111111, 999999999).ToString();

            var departureDateTime = now.AddDays(rand.Next(1, 365)).AddHours(rand.Next(1, 24)).AddMinutes(rand.Next(1, 60));
            var arrivalDateTime = departureDateTime.AddHours(rand.Next(1, 24)).AddMinutes(rand.Next(1, 60));

            var departurePortindex = rand.Next(0, owner.AvaliableAirports.Length);
            var departurePort = owner.AvaliableAirports[departurePortindex];

            var index = rand.Next(0, departurePort.AvaliableTerminals.Length);
            var departureTerminal = departurePort.AvaliableTerminals[index];

            index = rand.Next(0, departureTerminal.AvaliableGates.Length);
            var departureGate = departureTerminal.AvaliableGates[index];

            do
            {
                index = rand.Next(0, owner.AvaliableAirports.Length);
            } while (index == departurePortindex);

            var arrivalPort = owner.AvaliableAirports[index];

            index = rand.Next(0, arrivalPort.AvaliableTerminals.Length);
            var arrivalTerminal = arrivalPort.AvaliableTerminals[index];

            index = rand.Next(0, arrivalTerminal.AvaliableGates.Length);
            var arrivalGate = arrivalTerminal.AvaliableGates[index];

            var flightStatus = (FlightStatuses)rand.Next(1, ServiceHelper.GetEnumValues(typeof(FlightStatuses)).Length + 1);

            return new Flight(owner,
                flightNumber,
                departureDateTime,
                arrivalDateTime,
                departurePort,
                arrivalPort,
                flightStatus,
                departureTerminal,
                arrivalTerminal,
                departureGate,
                arrivalGate);
        }

        public override string ToString()
        {
            return $"{this.FlightNumber} {this.DeparturePort} - {this.ArrivalPort}";
        }
        public static void EditFlight(Flight flight)
        {
            if (flight == null)
                return;

            while (true)
            {
                var fieldToEdit = (FlightEditedFields)UserInteraction.ChooseEnumValue("Choose field to edit", typeof(FlightEditedFields));

                switch (fieldToEdit)
                {
                    case FlightEditedFields.FinishEditing:
                        return;

                    case FlightEditedFields.DepartureDateTime:

                        flight.DepartureDateTime = UserInteraction.GetDate($"Curent date: {flight.DepartureDateTime}, Enter new date");
                        break;

                    case FlightEditedFields.ArrivalDateTime:

                        flight.ArrivalDateTime = UserInteraction.GetDate($"Curent date: {flight.ArrivalDateTime}, Enter new date");
                        break;

                    case FlightEditedFields.DepartureTerminal:

                        flight.DepartureTerminal = (Terminals)UserInteraction.ChooseElemementFromArray(flight.DeparturePort.AvaliableTerminals, $"Current terminal {flight.DepartureTerminal}, Choose new terminal");
                        flight.DepartureGate = (Gates)UserInteraction.ChooseElemementFromArray(flight.DepartureTerminal.AvaliableGates, "Choose new departure gate");

                        break;

                    case FlightEditedFields.ArrivalTerminal:

                        flight.ArrivalTerminal = (Terminals)UserInteraction.ChooseElemementFromArray(flight.ArrivalPort.AvaliableTerminals, $"Current terminal {flight.ArrivalTerminal}, Choose new terminal");
                        flight.ArrivalGate = (Gates)UserInteraction.ChooseElemementFromArray(flight.ArrivalTerminal.AvaliableGates, "Choose new arrival gate");

                        break;

                    case FlightEditedFields.DepartureGate:

                        flight.DepartureGate = (Gates)UserInteraction.ChooseElemementFromArray(flight.DepartureTerminal.AvaliableGates, $"Current gate {flight.DepartureGate} Choose new departure gate");
                        break;

                    case FlightEditedFields.ArrivalGate:

                        flight.ArrivalGate = (Gates)UserInteraction.ChooseElemementFromArray(flight.ArrivalTerminal.AvaliableGates, $"Current gate {flight.ArrivalGate} Choose new arrival gate");
                        break;

                    case FlightEditedFields.FlightStatus:

                        flight.FlightStatus = (FlightStatuses)UserInteraction.ChooseEnumValue($"Current status {flight.FlightStatus} Choose new status", typeof(FlightStatuses));
                        break;

                    default:
                        Console.WriteLine($"Edit for field {fieldToEdit} not implemented");
                        break;
                }
            }
        }
    }
}
