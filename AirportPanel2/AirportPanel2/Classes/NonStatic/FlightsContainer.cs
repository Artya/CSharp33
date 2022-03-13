using System;

namespace AirportPanel2
{
    public class FlightsContainer : ITableContainer
    {
        private readonly int bordersCount = 12;
        public int FlightNumberColumnWidth { get; set; } = 15;
        public int DateTimeColumnWidth { get; set; } = 10;
        public int AirporColumntWidth { get; set; } = 10;
        public int TerminalColumnWidth { get; set; } = 8;
        public int GateColumnWidth { get; set; } = 8;
        public int StatusColumnWidth { get; set; } = 15;
        public int Length { get { return FlightsArray == null ? 0 : FlightsArray.Length; } }
        public int TableWidth { get; private set; }
        public Flight[] FlightsArray { get; private set;}
        public Airline Airline { get; }
        public string TableName => "Flights";

        public FlightsContainer(Airline airline)
        {
            this.Airline = airline;
        }

        public void PrintTableRow(int rowIndex, ConsoleColor borderColor)
        {
            var flight = FlightsArray[rowIndex];

            TablePrinter.WriteCell(flight.FlightNumber.ToString(), this.FlightNumberColumnWidth, borderColor);

            TablePrinter.WriteCell(flight.DepartureDateTime.ToString(Constants.DateTimeFormatString), this.DateTimeColumnWidth, borderColor);
            TablePrinter.WriteCell(flight.DeparturePort.ToString(), this.AirporColumntWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(flight.DepartureTerminal.ToString(), this.TerminalColumnWidth, borderColor);
            TablePrinter.WriteCell(flight.DepartureGate.ToString(), this.GateColumnWidth, borderColor, CellTextLevelling.Left);

            TablePrinter.WriteCell(flight.ArrivalDateTime.ToString(Constants.DateTimeFormatString), this.DateTimeColumnWidth, borderColor);
            TablePrinter.WriteCell(flight.ArrivalPort.ToString(), this.AirporColumntWidth, borderColor, CellTextLevelling.Left);
            TablePrinter.WriteCell(flight.ArrivalTerminal.ToString(), this.TerminalColumnWidth, borderColor);
            TablePrinter.WriteCell(flight.ArrivalGate.ToString(), this.GateColumnWidth, borderColor, CellTextLevelling.Left);

            var flightStatusRepresentation = ServiceHelper.ConvertFromCamelCaseToUsualText(flight.FlightStatus.ToString());
            TablePrinter.WriteCell(flightStatusRepresentation, this.StatusColumnWidth, borderColor, CellTextLevelling.Left);

            var duration = (flight.ArrivalDateTime - flight.DepartureDateTime);
            TablePrinter.WriteCell($"{duration.Hours.ToString("00")}:{duration.Minutes.ToString("00")}", Constants.DurationColumnWidth, borderColor, CellTextLevelling.Center);

            Console.WriteLine();
        }

        public void PrintTableTitle(ConsoleColor borderColor)
        {
            var commonAirportWidth = this.DateTimeColumnWidth
                                       + Constants.BorderWidth
                                       + this.AirporColumntWidth
                                       + Constants.BorderWidth
                                       + this.TerminalColumnWidth
                                       + Constants.BorderWidth
                                       + this.GateColumnWidth;

            TablePrinter.WriteCell(string.Empty, this.FlightNumberColumnWidth, borderColor);
            TablePrinter.WriteCell("-DEPARTURE-", commonAirportWidth, borderColor);
            TablePrinter.WriteCell("-ARRIVAL-", commonAirportWidth, borderColor);
            TablePrinter.WriteCell(string.Empty, this.StatusColumnWidth, borderColor);
            TablePrinter.WriteCell(string.Empty, Constants.DurationColumnWidth, borderColor);
            Console.WriteLine();
            TablePrinter.WriteHorisontalBorder(this.TableWidth, '-', borderColor);
            TablePrinter.WriteVerticalBorder(borderColor);
            TablePrinter.WriteCell("Flight number", this.FlightNumberColumnWidth, borderColor);
            TablePrinter.WriteCell("Date", this.DateTimeColumnWidth, borderColor);
            TablePrinter.WriteCell("Airport", this.AirporColumntWidth, borderColor);
            TablePrinter.WriteCell("Terminal", this.TerminalColumnWidth, borderColor);
            TablePrinter.WriteCell("Gate", this.GateColumnWidth, borderColor);
            TablePrinter.WriteCell("Date", this.DateTimeColumnWidth, borderColor);
            TablePrinter.WriteCell("Airport", this.AirporColumntWidth, borderColor);
            TablePrinter.WriteCell("Terminal", this.TerminalColumnWidth, borderColor);
            TablePrinter.WriteCell("Gate", this.GateColumnWidth, borderColor);
            TablePrinter.WriteCell("Flight status", this.StatusColumnWidth, borderColor);
            TablePrinter.WriteCell("Duration", Constants.DurationColumnWidth, borderColor);
            Console.WriteLine();
        }

        public void AddFlight(Flight newFlight)
        {
            this.FlightsArray = (Flight[])ServiceHelper.AddElementToArray(this.FlightsArray, typeof(Flight), newFlight);

            FlightNumberColumnWidth = Math.Max(FlightNumberColumnWidth, newFlight.FlightNumber.Length);
            DateTimeColumnWidth = Math.Max(DateTimeColumnWidth, newFlight.DepartureDateTime.ToString().Length);
            DateTimeColumnWidth = Math.Max(DateTimeColumnWidth, newFlight.ArrivalDateTime.ToString().Length);
            AirporColumntWidth = Math.Max(AirporColumntWidth, newFlight.DeparturePort.Name.Length);
            AirporColumntWidth = Math.Max(AirporColumntWidth, newFlight.ArrivalPort.Name.Length);
            TerminalColumnWidth = Math.Max(TerminalColumnWidth, newFlight.DepartureTerminal.Name.Length);
            TerminalColumnWidth = Math.Max(TerminalColumnWidth, newFlight.ArrivalTerminal.Name.Length);
            GateColumnWidth = Math.Max(GateColumnWidth, newFlight.DepartureGate.Name.Length);
            GateColumnWidth = Math.Max(GateColumnWidth, newFlight.ArrivalGate.Name.Length);
            StatusColumnWidth = Math.Max(StatusColumnWidth, newFlight.FlightStatus.ToString().Length);

            RecalculateTotalTableWidth();
        }

        public void WorkWithMenu()
        {
            while (true)
            {
                var operation = (FlightMenu)UserInteraction.ChooseEnumValue("Flight menu:", typeof(FlightMenu));

                switch (operation)
                {
                    case FlightMenu.ReturnToMainMenu:
                        return;

                    case FlightMenu.ShowFlights:
                        TablePrinter.PrintTable(this, ConsoleColor.Green);
                        break;

                    case FlightMenu.AddNewFlight:

                        var newFlight = Flight.GetNewFlightFromUser(this.Airline);
                        this.AddFlight(newFlight);

                        break;

                    case FlightMenu.DeleteFlight:

                        var flightToDelete = (Flight)UserInteraction.ChooseElemementFromArray(this.FlightsArray, "Choose flight to delete");
                        this.FlightsArray = (Flight[])ServiceHelper.DeleteElementFromArray(this.FlightsArray, flightToDelete, typeof(Flight));

                        break;

                    case FlightMenu.ChangeFlight:

                        var flightToEdit = (Flight)UserInteraction.ChooseElemementFromArray(this.FlightsArray, "Choose flight to modify");
                        Flight.EditFlight(flightToEdit);

                        break;

                    default:
                        Console.WriteLine($"Operation {operation} non implemented");
                        break;
                }
            }
        }
        private void RecalculateTotalTableWidth()
        {
            var sum = this.bordersCount * Constants.BorderWidth;
            sum += FlightNumberColumnWidth;
            sum += DateTimeColumnWidth * 2;
            sum += AirporColumntWidth * 2;
            sum += TerminalColumnWidth * 2;
            sum += GateColumnWidth * 2;
            sum += StatusColumnWidth;
            sum += Constants.DurationColumnWidth;

            TableWidth = sum;
        }

        public FlightsContainer SearchByFlightNumber()
        {
            var flightNumber = UserInteraction.GetString();
            var newContainer = new FlightsContainer(this.Airline);

            foreach (var flight in this.FlightsArray)
            {
                if (flight.FlightNumber == flightNumber)
                    newContainer.AddFlight(flight);
            }

            return newContainer;
        }

        public FlightsContainer SearchByAirport(SearchTypes searchType)
        {
            var newContainer = new FlightsContainer(this.Airline);

            var searchedPort = UserInteraction.ChooseElemementFromArray(this.Airline.AvaliableAirports, "Choose airport");

            foreach (var flight in this.FlightsArray)
            {
                var addItem = false;

                switch (searchType)
                {
                    case SearchTypes.ByDeparturePort:
                        addItem = searchedPort == flight.DeparturePort;
                        break;

                    case SearchTypes.ByArrivalPort:
                        addItem = searchedPort == flight.ArrivalPort;
                        break;
                }

                if (addItem)
                    newContainer.AddFlight(flight);
            }
            
            return newContainer;
        }
    }
}
