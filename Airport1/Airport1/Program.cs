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

            airportTable = FillRandomFlights(airportTable);

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
                    case OperationType.DisplayTable:
                        DisplayTable(airportTable);
                        break;

                    case OperationType.AddFlight:
                        airportTable = AddFlight(airportTable);
                        break;

                    case OperationType.ShowArrival:
                        ShowArrivals(airportTable);
                        break;

                    case OperationType.ShowDepartures:
                        ShowDepartures(airportTable);
                        break;

                    case OperationType.EditInformation:
                        EditInformation(airportTable);
                        break;

                    case OperationType.SearchByFlightNumber:
                        SearchByFlightNumber(airportTable);
                        break;

                    case OperationType.SearchByTime:
                        SearchByTime(airportTable);
                        break;

                    case OperationType.Exit:
                        return;

                    default:
                        Console.WriteLine($"Program doesn't has operation type id {(int)operationType}");
                        break;
                }
            }
        }

        private static string[,] FillRandomFlights(string[,] airportTable)
        {
            var preDefinedFligtsCount = 20;

            for (var i = 0; i < preDefinedFligtsCount; i++)
            {
                airportTable = AddRandomFlight(airportTable);
            }

            DisplayTable(airportTable);
            return airportTable;
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
            SetValueToCell(table, rowToFill, TableCoulumns.FlightStatus, "Flight status");
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

                if (rowIndex != rowCount - 1)
                {
                    WriteHorisontalBorder('-', tableBorderColor);
                }
            }

            WriteHorisontalBorder('=', tableBorderColor);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string[,] AddFlight(string[,] table)
        {
            table = IncreaseOrCreateTable(table, 1);
            var currentRowIndex = table.GetLength(0) - 1;
            var convertToInt = true;
            var convertToDate = true;

            SetValueToCell(table, currentRowIndex, TableCoulumns.RowNum, currentRowIndex.ToString());

            var flightType = (FlightType)int.Parse(GetDataFromUser("Enter flight type:", convertToInt, !convertToDate, typeof(FlightType)));
            SetValueToCell(table, currentRowIndex, TableCoulumns.FlightType, flightType.ToString());

            var dateTime = GetDataFromUser("Enter flight date/time in format dd/mm/yy hh:mm", !convertToInt, convertToDate);
            SetValueToCell(table, currentRowIndex, TableCoulumns.DateAndTime, dateTime.ToString());

            var flightNumber = GetDataFromUser("Enter flight number");
            SetValueToCell(table, currentRowIndex, TableCoulumns.FlightNumber, flightNumber);

            var cityPort = (Airpots)int.Parse(GetDataFromUser("Enter City port of arrival/departure:", convertToInt, !convertToDate, typeof(Airpots)));
            SetValueToCell(table, currentRowIndex, TableCoulumns.CityPort, cityPort.ToString());

            var airline = (Airlines)int.Parse(GetDataFromUser("Enter airline", convertToInt, !convertToDate, typeof(Airlines)));
            SetValueToCell(table, currentRowIndex, TableCoulumns.AirLine, airline.ToString());

            var terminal = GetDataFromUser("Enter terminal");
            SetValueToCell(table, currentRowIndex, TableCoulumns.Terminal, terminal);

            var flightStatus = (FlightStatuses)int.Parse(GetDataFromUser(@"Enter flight status:", convertToInt, !convertToDate, typeof(FlightStatuses)));
            SetValueToCell(table, currentRowIndex, TableCoulumns.FlightStatus, flightStatus.ToString());

            var gate = GetDataFromUser("Enter Gate");
            SetValueToCell(table, currentRowIndex, TableCoulumns.Gate, gate);

            return table;
        }

        public static string GetDataFromUser(string title, bool parseToInt = false, bool parseToDate = false, Type enumerationType = null)
        {
            var maxIndex = int.MinValue;
            var minIndex = int.MaxValue;

            if (enumerationType != null)
            {
                title += Environment.NewLine;                

                foreach (var currentEnumValue in Enum.GetValues(enumerationType))
                {
                    var indexOfEnum = (int)currentEnumValue;
                    maxIndex = Math.Max(maxIndex, indexOfEnum);
                    minIndex = Math.Min(minIndex, indexOfEnum);

                    title += $"{indexOfEnum} - {currentEnumValue} " + Environment.NewLine;
                }
            }

            while (true)
            {
                Console.WriteLine(title);

                var input = Console.ReadLine();

                if (!parseToDate && !parseToInt)
                    return input;

                if (parseToInt)
                {
                    try
                    {
                        var result = int.Parse(input);

                        if (enumerationType != null 
                            && (result < minIndex || result > maxIndex))
                        {
                            Console.WriteLine("Entered value is out of range, try again");
                            continue;
                        }

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
            }
        }
        public static void ShowArrivals(string[,] airportTable)
        {
            FilterTableByColumnValue(airportTable, TableCoulumns.FlightType, FlightType.Arrival.ToString());
        }
        public static void ShowDepartures(string[,] airportTable)
        {
            FilterTableByColumnValue(airportTable, TableCoulumns.FlightType, FlightType.Departure.ToString());
        }
        public static void EditInformation(string[,] airportTable)
        {
            var maxRow = airportTable.GetLength(0) - 1;
            var convertToInt = true;
            var convertToBool = true;

            if (maxRow < 1)
            {
                Console.WriteLine("Table is empty..");
                return;
            }

            var title = $"Enter a row number, from 1 to {maxRow}";
            var row = int.Parse(GetDataFromUser(title, convertToInt));

            if (row < 1 || row > maxRow)
            {
                Console.WriteLine($"Row {row} isn`t exist");
                return;
            }

            title = $@"Choose a column to edit
{(int)TableCoulumns.FlightType} Flight type
{(int)TableCoulumns.DateAndTime} Date and time
{(int)TableCoulumns.FlightNumber} Flight number
{(int)TableCoulumns.CityPort} City / port
{(int)TableCoulumns.AirLine} Airline
{(int)TableCoulumns.Terminal} Terminal
{(int)TableCoulumns.FlightStatus} Flight status
{(int)TableCoulumns.Gate} Gate";

            var column = (TableCoulumns)int.Parse(GetDataFromUser(title, convertToInt));

            title = $"Current value is: {airportTable[row, (int)column]}, Enter new value please:";
            var newValue = string.Empty;

            switch (column)
            {
                case TableCoulumns.DateAndTime:
                    newValue = GetDataFromUser(title, !convertToInt, convertToBool);
                    break;
                case TableCoulumns.FlightType:
                    newValue = ((FlightType)int.Parse(GetDataFromUser(title, convertToInt, !convertToBool))).ToString();
                    break;
                default:
                    newValue = GetDataFromUser(title);
                    break;
            }

            SetValueToCell(airportTable, row, column, newValue);
        }
        public static void SearchByFlightNumber(string[,] airportTable)
        {
            var searchingFlightNumber = GetDataFromUser("Enter flight number to search");
            FilterTableByColumnValue(airportTable, TableCoulumns.FlightNumber, searchingFlightNumber);
        }
        public static void SearchByTime(string[,] airportTable)
        {
            var title = @$"Choose filter type:
{(int)TimeFilters.Hour} One hour
{(int)TimeFilters.Day} One day
{(int)TimeFilters.Week} One week
{(int)TimeFilters.Month} One month
{(int)TimeFilters.HalfAYear} Six months
{(int)TimeFilters.Year} One Year";

            var choice = (TimeFilters)int.Parse(GetDataFromUser(title, true));

            var filterDate = DateTime.Now;

            switch (choice)
            {
                case TimeFilters.Hour:
                    filterDate = filterDate.AddHours(1);
                    break;
                case TimeFilters.Day:
                    filterDate = filterDate.AddDays(1);
                    break;
                case TimeFilters.Week:
                    filterDate = filterDate.AddDays(7);
                    break;
                case TimeFilters.Month:
                    filterDate = filterDate.AddMonths(1);
                    break;
                case TimeFilters.HalfAYear:
                    filterDate = filterDate.AddMonths(6);
                    break;
                case TimeFilters.Year:
                    filterDate = filterDate.AddYears(1);
                    break;
                default:
                    Console.WriteLine("Incorrect filter type, table no filtered");
                    DisplayTable(airportTable);
                    return;
            }

            FilterTableByColumnValue(airportTable, TableCoulumns.DateAndTime, filterDate.ToString(), ComparisonTypes.GreaterOrEqual);
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
            SetValueToCell(table, rowToFill, TableCoulumns.DateAndTime, dateTime.ToString("dd.MM.yyyy hh:mm"));

            var flightNumber = rand.Next(100, 100000);
            SetValueToCell(table, rowToFill, TableCoulumns.FlightNumber, flightNumber.ToString());

            var cityPort = (Airpots)rand.Next(0, Enum.GetValues(typeof(Airpots)).Length);
            SetValueToCell(table, rowToFill, TableCoulumns.CityPort, cityPort.ToString());

            var fligtStatus = (FlightStatuses)rand.Next(0, Enum.GetValues(typeof(FlightStatuses)).Length);
            SetValueToCell(table, rowToFill, TableCoulumns.FlightStatus, fligtStatus.ToString());

            var terminals = "ABCDEF";
            var terminal = terminals[rand.Next(0, terminals.Length)];
            SetValueToCell(table, rowToFill, TableCoulumns.Terminal, terminal.ToString());

            var gate = rand.Next(1, 50);
            SetValueToCell(table, rowToFill, TableCoulumns.Gate, gate.ToString());

            var airLine = (Airlines)rand.Next(0, Enum.GetValues(typeof(Airlines)).Length);
            SetValueToCell(table, rowToFill, TableCoulumns.AirLine, airLine.ToString());

            return table;
        }

        public static void FilterTableByColumnValue(string[,] airportTable, TableCoulumns columnToSearch, string searchingValue, ComparisonTypes comparisonType = ComparisonTypes.Equal)
        {
            var newTable = IncreaseOrCreateTable(null, 1);
            FillTableTitle(newTable);

            var tableHeight = airportTable.GetLength(0);

            for (var rowIndex = 1; rowIndex < tableHeight; rowIndex++)
            {
                var rowIsFitCondition = false;
                var intColumnToSearch = (int)columnToSearch;

                switch (comparisonType)
                {
                    case ComparisonTypes.Equal:
                        rowIsFitCondition = airportTable[rowIndex, intColumnToSearch] == searchingValue;
                        break;
                    case ComparisonTypes.NoEqual:
                        rowIsFitCondition = airportTable[rowIndex, intColumnToSearch] != searchingValue;
                        break;
                    case ComparisonTypes.GreaterOrEqual:
                        var searchingDate = DateTime.Parse(searchingValue);
                        var rowDate = DateTime.Parse(airportTable[rowIndex, intColumnToSearch] + ":00");
                        rowIsFitCondition = searchingDate >= rowDate;
                        break;
                }

                if (rowIsFitCondition)
                {
                    newTable = IncreaseOrCreateTable(newTable, 1);
                    var lastRowOfNewTable = newTable.GetLength(0) - 1;

                    for (var columnIndex = 0; columnIndex < GetTableColumnsCount(); columnIndex++)
                    {

                        var currentColumn = (TableCoulumns)columnIndex;

                        SetValueToCell(newTable, lastRowOfNewTable, currentColumn, airportTable[rowIndex, columnIndex]);
                    }
                }
            }

            DisplayTable(newTable);

            if (newTable.GetLength(0) <= 1)
            {
                var tempForegroundColor = Console.ForegroundColor;
                var tempBackGroundColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No results......");
                Console.ForegroundColor = tempForegroundColor;
                Console.BackgroundColor = tempBackGroundColor;
            }
        }
    }
}
