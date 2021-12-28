using System;
using System.Globalization;

namespace Airport1
{
    class Program
    {
        static int[] maxStringLengthesInTable = new int[GetTableColumnsCount()];
        static void Main(string[] args)
        {
            const int tableAdditionPortion = 1;

            var airportTable = IncreaseOrCreateTable(null, tableAdditionPortion);
            FillTableTitle(airportTable);

            var preDefinedFligtsCount = 15;

            for (var i = 0; i < preDefinedFligtsCount; i++)
            {
                airportTable = AddRandomFlight(airportTable);
            }

            while (true)
            {
                Console.WriteLine(@$"Please choose operation.
{(int)OperationType.DisplayTable} - Display table
{(int)OperationType.AddFlight} - Add flight
{(int)OperationType.ShowArrival} - Arrivals
{(int)OperationType.ShowDepartures} - Departures
{(int)OperationType.EditInformation} - Edit inforamtion
{(int)OperationType.SearchByFlightNumber} - Search by flight number
{(int)OperationType.SearchByTime} - Search by specified time in airport
{(int)OperationType.Exit} - Exit");

                var operationType = (OperationType)int.Parse(Console.ReadLine());
                switch (operationType)
                {
                    case OperationType.DisplayTable: DisplayTable(airportTable); break;
                    case OperationType.AddFlight: airportTable = AddFlight(airportTable); break;
                    case OperationType.ShowArrival: ShowArrivals(); break;
                    case OperationType.ShowDepartures: ShowDepartures(); break;
                    case OperationType.EditInformation: EditInformation(); break;
                    case OperationType.SearchByFlightNumber: SearchByFlightNumber(); break;
                    case OperationType.SearchByTime: SearchByTime(); break;
                    case OperationType.Exit: return;
                    default:
                        Console.WriteLine($"Program doesn't has operation type id {(int)operationType}");
                        break;
                }
            }
        }

        public static int GetTableColumnsCount()
        {
            // знаю шо ми таке ще не вчили, але серцем відчував що таке щось має бути, і не помилився
            return Enum.GetValues(typeof(TableCoulumns)).Length;
        }

        public static void FillTableTitle(string[,] table)
        {
            int rowToFill = 0;
            SetValueToCell(table, rowToFill, TableCoulumns.RowNum, "#");
            SetValueToCell(table, rowToFill, TableCoulumns.FlightType, "Flight type");
            SetValueToCell(table, rowToFill, TableCoulumns.AirLine, "Airline");
            SetValueToCell(table, rowToFill, TableCoulumns.CityPort, "City / Port");
            SetValueToCell(table, rowToFill, TableCoulumns.DateAndTime, "Date / Time");
            SetValueToCell(table, rowToFill, TableCoulumns.FightStatus, "Flight status");
            SetValueToCell(table, rowToFill, TableCoulumns.FlightNumber, "Flight number");
            SetValueToCell(table, rowToFill, TableCoulumns.Gate, "Gate");
            SetValueToCell(table, rowToFill, TableCoulumns.Terminal, "Terminal");
        }

        public static void SetValueToCell(string[,] airportTable, int row, TableCoulumns column, string cellValue)
        {
            if (airportTable.GetLength(0) <= row)
            {
                Console.WriteLine("Method SetValueToCell got incorrect row number, it`s out of range");
                return;
            }

            var intColumn = (int)column;

            airportTable[row, intColumn] = cellValue;

            if (cellValue.Length > maxStringLengthesInTable[intColumn])
            {
                maxStringLengthesInTable[intColumn] = cellValue.Length;
            }
        }
        /// <summary>
        /// Creating new airport table or increase existing 
        /// </summary>
        /// <param name="currentTable">null or existing tabe</param>
        /// <param name="rowCount">Table increasing step</param>
        /// <returns>Airport table</returns>
        public static string[,] IncreaseOrCreateTable(string[,] currentTable, int rowCount)
        {
            var columnCount = GetTableColumnsCount();
            var currentTableRowCount = currentTable == null ? 0 : currentTable.GetLength(0);
            var newRowCount = currentTableRowCount + rowCount;

            var newtable = new string[newRowCount, columnCount];

            if (currentTable == null)
                return newtable;

            for (var i = 0; i < currentTableRowCount; i++)
            {
                for (var j = 0; j < columnCount; j++)
                {
                    newtable[i, j] = currentTable[i, j];
                }
            }

            return newtable;
        }

        public static void DisplayTable(string[,] airportTable)
        {
            var tableWidth = Math.Min(GetTableWidth(), Console.LargestWindowWidth);

            if (tableWidth > Console.WindowWidth || tableWidth > Console.BufferWidth)
            {
                Console.BufferWidth = tableWidth;
                Console.WindowWidth = tableWidth;
            }

            var rowCount = airportTable.GetLength(0);
            var columnCount = airportTable.GetLength(1);
            var tableBorderColor = ConsoleColor.Green;

            Console.ForegroundColor = tableBorderColor;

            WriteHorisontalBorder('=', tableBorderColor);

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                if (rowIndex > 0)
                    Console.ForegroundColor = airportTable[rowIndex, (int)TableCoulumns.FlightType] == FlightType.Arrival.ToString() ? ConsoleColor.Yellow : ConsoleColor.Blue;

                WriteVerticalBorder(tableBorderColor);

                for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    Console.Write(airportTable[rowIndex, columnIndex]);

                    var currentSpaceCount = maxStringLengthesInTable[columnIndex] - (airportTable[rowIndex, columnIndex] == null ? 0 : airportTable[rowIndex, columnIndex].Length);

                    for (var spaceIndex = 0; spaceIndex < (currentSpaceCount); spaceIndex++)
                        Console.Write(" ");

                    WriteVerticalBorder(tableBorderColor);
                }

                Console.WriteLine();

                if (rowIndex == 0)
                {
                    WriteHorisontalBorder('=', tableBorderColor);
                    continue;
                }

                WriteHorisontalBorder('-', tableBorderColor);
            }

            WriteHorisontalBorder('=', tableBorderColor);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string[,] AddFlight(string[,] table)
        {

            table = IncreaseOrCreateTable(table, 1);
            var currentRowIndex = table.GetLength(0) - 1;

            var title = @$"Enter flight type:
{(int)FlightType.Arrival} - Arrival
{(int)FlightType.Departure} - Departure";

            var convertToInt = true;
            var convertToDate = true;

            SetValueToCell(table, currentRowIndex, TableCoulumns.RowNum, currentRowIndex.ToString());

            var flightType = (FlightType)int.Parse(GetDataFromUser(title, convertToInt, !convertToDate));
            SetValueToCell(table, currentRowIndex, TableCoulumns.FlightType, flightType.ToString());

            var dateTime = GetDataFromUser("Enter flight date/time in format dd/mm/yy hh:mm", !convertToInt, convertToDate);
            SetValueToCell(table, currentRowIndex, TableCoulumns.DateAndTime, dateTime.ToString());

            var flightNumber = GetDataFromUser("Enter flight number");
            SetValueToCell(table, currentRowIndex, TableCoulumns.FlightNumber, flightNumber);

            var cityPort = GetDataFromUser("Enter City port of arrival/departure");
            SetValueToCell(table, currentRowIndex, TableCoulumns.CityPort, cityPort);

            var airline = GetDataFromUser("Enter airline");
            SetValueToCell(table, currentRowIndex, TableCoulumns.AirLine, airline);

            var terminal = GetDataFromUser("Enter terminal");
            SetValueToCell(table, currentRowIndex, TableCoulumns.Terminal, terminal);

            var flightStatus = GetDataFromUser("Enter flight status");
            SetValueToCell(table, currentRowIndex, TableCoulumns.FightStatus, flightStatus);

            var gate = GetDataFromUser("Enter Gate");
            SetValueToCell(table, currentRowIndex, TableCoulumns.Gate, gate);

            return table;
        }

        public static string GetDataFromUser(string title, bool parseToInt = false, bool parseToDate = false)
        {

            while (true)
            {
                Console.WriteLine(title);

                var input = Console.ReadLine();

                if (parseToInt)
                {
                    try
                    {
                        var result = int.Parse(input);
                        return result.ToString();
                    }
                    catch
                    {
                        Console.WriteLine($"Error converting {input} to number, try again.");
                        continue;
                    }
                }

                if (parseToDate)
                {
                    try
                    {
                        var result = DateTime.Parse(input);
                        return result.ToString();
                    }
                    catch
                    {
                        Console.WriteLine($"Error converting {input} to Date / Time, try again.");
                        continue;
                    }
                }

                return input;
            }
        }
        public static void ShowArrivals()
        {
            Console.WriteLine("ShowArrivals");
        }
        public static void ShowDepartures()
        {
            Console.WriteLine("ShowDepartures");
        }
        public static void EditInformation()
        {
            Console.WriteLine("EditInformation");
        }
        public static void SearchByFlightNumber()
        {
            Console.WriteLine("SearchByFlightNumber");
        }
        public static void SearchByTime()
        {
            Console.WriteLine("SearchByTime");
        }

        public static int GetTableWidth()
        {
            var width = 0;
            var columnBorderWidh = 3;
            for (var i = 0; i < maxStringLengthesInTable.Length; i++)
            {
                width += maxStringLengthesInTable[i];
                width += columnBorderWidh;
            }

            width += columnBorderWidh;

            return width;
        }

        public static void WriteHorisontalBorder(char symbol, ConsoleColor tableBorderColor)
        {
            var tableWidth = GetTableWidth();
            var tempColor = Console.ForegroundColor;
            Console.ForegroundColor = tableBorderColor;
            Console.WriteLine(" " + new string(symbol, tableWidth - 2) + " ");
            Console.ForegroundColor = tempColor;
        }

        public static void WriteVerticalBorder(ConsoleColor tableBorderColor)
        {
            var tempColor = Console.ForegroundColor;
            Console.ForegroundColor = tableBorderColor;
            Console.Write(" | ");
            Console.ForegroundColor = tempColor;
        }

        public static string[,] AddRandomFlight(string[,] table)
        {
            var rand = new Random();

            table = IncreaseOrCreateTable(table, 1);
            var rowToFill = table.GetLength(0) - 1;
            SetValueToCell(table, rowToFill, TableCoulumns.RowNum, rowToFill.ToString());

            var flightType = (FlightType)rand.Next(1, 3);
            SetValueToCell(table, rowToFill, TableCoulumns.FlightType, flightType.ToString());

            var dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(rand.Next(1, 365));
            dateTime = dateTime.AddHours(rand.Next(1, 25));
            dateTime = dateTime.AddMinutes(rand.Next(1, 60));
            SetValueToCell(table, rowToFill, TableCoulumns.DateAndTime, dateTime.ToString());

            var flightNumber = rand.Next(100, 100000);
            SetValueToCell(table, rowToFill, TableCoulumns.FlightNumber, flightNumber.ToString());

            var cityPort = (Airpots)rand.Next(0, Enum.GetValues(typeof(Airpots)).Length);
            SetValueToCell(table, rowToFill, TableCoulumns.CityPort, cityPort.ToString());

            var fligtStatus = (FlightStatuses)rand.Next(0, Enum.GetValues(typeof(FlightStatuses)).Length);
            SetValueToCell(table, rowToFill, TableCoulumns.FightStatus, fligtStatus.ToString());

            var terminals = "ABCDEF";
            var terminal = terminals[rand.Next(0, terminals.Length)];
            SetValueToCell(table, rowToFill, TableCoulumns.Terminal, terminal.ToString());

            var gate = rand.Next(1, 50);
            SetValueToCell(table, rowToFill, TableCoulumns.Gate, gate.ToString());

            var airLine = (Airlines)rand.Next(0, Enum.GetValues(typeof(Airlines)).Length);
            SetValueToCell(table, rowToFill, TableCoulumns.AirLine, airLine.ToString());

            return table;
        }
    }
}
