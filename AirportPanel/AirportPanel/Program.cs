using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Globalization;

partial class Program
{

    public static readonly int[] fieldsWidth = { 8, 10, 20, 16, 18, 4, 11, 16 };
    public static readonly string[] flightStatuses = { "CHECK-IN", "GATE CLOSED", "ARRIVED", "DEPARTED AT", "UNKNOWN", "CANCELED", "EXPECTED AT", "DELAYED", "IN FLIGHT"};
    public enum DateTimePositionOffsetEnum
    {
        Day = 0,
        Month = 3,
        Year = 6,
        Hour = 11,
        Mitutes = 14
    };
    private enum SearchFieldEnum
    {
        FlightNumber,
        CityPort,
        DateTimeOf,
        SearchNearest
    }
    const char whiteSpace = (char)32;
    const int rowLength = 120;
    public static char[][] consoleBufferChar;
    public static (ConsoleColor background, ConsoleColor foreground)[][] consoleBufferColor;

    public static void Main()
    {

        var departureFlights = new string[][]
            {
                new string[] { "A", "BA117", "Edinburgh", "31/12/2021 06:35", "WizzAir", "2", "DEPARTED AT", "22/12/2021 06:40" },
                new string[] { "D", "CX1347", "Bucharest", "31/12/2021 06:50", "RyanAir", "14", "GATE CLOSED", "" },
                new string[] { "B", "BA165", "Brussels", "31/12/2021 06:55", "WizzAir", "4", "CHECK-IN", "" },
                new string[] { "C", "BA3925", "Amsterdam", "31/12/2021 07:05", "WizzAir", "6", "CHECK-IN", "" },
                new string[] { "D", "BAB72", "Istambul", "31/12/2021 07:10", "RyanAir", "10", "DELAYED", "" },
                new string[] { "C", "AA6706", "Zurich", "31/12/2021 07:25", "WizzAir", "5", "CHECK-IN", "" },
                new string[] { "A", "DL6611CO", "London", "31/12/2021 07:35", "WizzAir", "8", "CANCELED", "" }
            };

        var arrivalFlights = new string[][]
            {
                new string[] { "B", "KP 102CO", "Munich", "30/12/2021 18:35", "WizzAir", "4", "ARRIVED", "" },
                new string[] { "A", "TY11Z5CO", "Budapest", "31/12/2021 07:15", "RyanAir", "22", "EXPECTED AT", "22/11/2021 07:25" },
                new string[] { "C", "SYS981RO", "New-York", "31/12/2021 08:30", "WizzAir", "18", "IN FLIGHT", "" },
                new string[] { "A", "AF12", "Lviv", "31/12/2021 08:35", "WizzAir", "6", "CANCELED", "" },
            };

        Console.SetBufferSize(252, 52);
        Console.SetWindowSize(252, 52);
        Console.CursorVisible = false;

        consoleBufferChar = new Char[52][];
        consoleBufferColor = new (ConsoleColor, ConsoleColor)[52][];

        for (int i = 0; i < consoleBufferChar.GetLength(0); i++)
        {
            consoleBufferChar[i] = new char[252];
            Array.Fill(consoleBufferChar[i], whiteSpace);
            consoleBufferColor[i] = new (ConsoleColor, ConsoleColor)[252];
            Array.Fill(consoleBufferColor[i], (ConsoleColor.Black, ConsoleColor.White));
        }

        var panelFlightsQuantity = 5;
        var activeDepartureIndex = 0;
        var activeArrivalIndex = 0;
        var activeFlights = FlightTypeEnum.Departure;
        var departureFirstItemIndex = 0;
        var arrivalFirstItemIndex = 0;
        var emergencyMessage = "";

        StartFieldsOutput();
        DisplayFlightsTable(departureFlights, 2, 5, 0, panelFlightsQuantity);
        DisplayFlightsTable(arrivalFlights, 131, 5, 0, panelFlightsQuantity);
        DisplayOrMoveBorders(BorderTypeEnum.Double, 0, 4, 120, 1);
        var input = new ConsoleKeyInfo();
        do
        {
            input = Console.ReadKey(true);
            var inputKey = input.Key;
            switch (inputKey)
            {
                case ConsoleKey.Tab:
                    if (activeFlights == FlightTypeEnum.Arrival)
                    {
                        var borderOldRowPos = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex);
                        var borderNewRowPos = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex);
                        DisplayOrMoveBorders(BorderTypeEnum.Double, 0, borderNewRowPos, 120, 1, true, 129, borderOldRowPos);
                        activeFlights = FlightTypeEnum.Departure;
                        break;
                    }
                    if (activeFlights == FlightTypeEnum.Departure)
                    {
                        var borderOldRowPos = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex);
                        var borderNewRowPos = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex);
                        DisplayOrMoveBorders(BorderTypeEnum.Double, 129, borderNewRowPos, 120, 1, true, 0, borderOldRowPos);
                        activeFlights = FlightTypeEnum.Arrival;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (activeFlights == FlightTypeEnum.Arrival)
                    {
                        var borderOldRowPos = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex);
                        var borderNewRowPos = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex);
                        DisplayOrMoveBorders(BorderTypeEnum.Double, 0, borderNewRowPos, 120, 1, true, 129, borderOldRowPos);
                        activeFlights = FlightTypeEnum.Departure;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (activeFlights == FlightTypeEnum.Departure)
                    {
                        var borderOldRowPos = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex);
                        var borderNewRowPos = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex);
                        DisplayOrMoveBorders(BorderTypeEnum.Double, 129, borderNewRowPos, 120, 1, true, 0, borderOldRowPos);
                        activeFlights = FlightTypeEnum.Arrival;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    {
                        switch (activeFlights)
                        {
                            case FlightTypeEnum.Departure:
                                if (activeDepartureIndex == departureFlights.GetLength(0) - 1)
                                    break;
                                if (activeDepartureIndex - departureFirstItemIndex + 1 < panelFlightsQuantity)
                                {
                                    var borderNewRowPos = 4 + 2 * (activeDepartureIndex + 1 - departureFirstItemIndex);
                                    DisplayOrMoveBorders(BorderTypeEnum.Double, 0, borderNewRowPos, 120, 1, true, 0, borderNewRowPos - 2);
                                }
                                else
                                {
                                    departureFirstItemIndex += 1;
                                    DisplayFlightsTable(departureFlights, 2, 5, departureFirstItemIndex, panelFlightsQuantity);
                                }
                                activeDepartureIndex += 1;
                                break;
                            case FlightTypeEnum.Arrival:
                                if (activeArrivalIndex == arrivalFlights.GetLength(0) - 1)
                                    break;
                                if (activeArrivalIndex - arrivalFirstItemIndex + 1 < panelFlightsQuantity)
                                {
                                    var borderNewRowPos = 4 + 2 * (activeArrivalIndex + 1 - arrivalFirstItemIndex);
                                    DisplayOrMoveBorders(BorderTypeEnum.Double, 129, borderNewRowPos, 120, 1, true, 129, borderNewRowPos - 2);
                                }
                                else
                                {
                                    arrivalFirstItemIndex += 1;
                                    DisplayFlightsTable(arrivalFlights, 131, 5, arrivalFirstItemIndex, panelFlightsQuantity);
                                }
                                activeArrivalIndex += 1;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConsoleKey.UpArrow:
                    {
                        switch (activeFlights)
                        {
                            case FlightTypeEnum.Departure:
                                if (activeDepartureIndex == 0)
                                    break;
                                if (activeDepartureIndex != departureFirstItemIndex)
                                {
                                    var borderNewRowPos = 4 + 2 * (activeDepartureIndex - 1 - departureFirstItemIndex);
                                    DisplayOrMoveBorders(BorderTypeEnum.Double, 0, borderNewRowPos, 120, 1, true, 0, borderNewRowPos + 2);
                                }
                                else
                                {
                                    departureFirstItemIndex -= 1;
                                    DisplayFlightsTable(departureFlights, 2, 5, departureFirstItemIndex, panelFlightsQuantity);
                                }
                                activeDepartureIndex -= 1;
                                break;
                            case FlightTypeEnum.Arrival:
                                if (activeArrivalIndex == 0)
                                    break;
                                if (activeArrivalIndex != arrivalFirstItemIndex)
                                {
                                    var borderNewRowPos = 4 + 2 * (activeArrivalIndex - 1 - arrivalFirstItemIndex);
                                    DisplayOrMoveBorders(BorderTypeEnum.Double, 129, borderNewRowPos, 120, 1, true, 129, borderNewRowPos + 2);
                                }
                                else
                                {
                                    arrivalFirstItemIndex -= 1;
                                    DisplayFlightsTable(arrivalFlights, 131, 5, arrivalFirstItemIndex, panelFlightsQuantity);
                                }
                                activeArrivalIndex -= 1;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConsoleKey.Enter:
                    {
                        switch (activeFlights)
                        {
                            case FlightTypeEnum.Departure:
                                EditFlight(departureFlights, activeDepartureIndex, 2, 5 + 2 * (activeDepartureIndex - departureFirstItemIndex));
                                DisplayFlightsTable(departureFlights, 2, 5, departureFirstItemIndex, panelFlightsQuantity);
                                break;
                            case FlightTypeEnum.Arrival: 
                                EditFlight(arrivalFlights, activeArrivalIndex, 131, 5 + 2 * (activeArrivalIndex - arrivalFirstItemIndex));
                                DisplayFlightsTable(arrivalFlights, 131, 5, arrivalFirstItemIndex, panelFlightsQuantity);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConsoleKey.Delete:
                    {
                        var deleteDialogColumn = 0;
                        var deleteDialogRow = 0;
                        switch (activeFlights)
                        {
                            case FlightTypeEnum.Departure:
                                deleteDialogColumn = 2 + 90;
                                deleteDialogRow = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex) + 1;
                                break;
                            case FlightTypeEnum.Arrival:
                                deleteDialogColumn = 131 + 90;
                                deleteDialogRow = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex) + 1;
                                break;
                            default:
                                break;
                        }
                        DisplayOrMoveBorders(BorderTypeEnum.Double, deleteDialogColumn, deleteDialogRow, 29, 1, false);
                        Console.SetCursorPosition(deleteDialogColumn + 1, deleteDialogRow + 1);
                        Console.ResetColor();
                        Console.Write("Delete this flight? ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("Y");
                        Console.ResetColor();
                        Console.Write("es / ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("N");
                        Console.ResetColor();
                        Console.Write("0 ");
                        var keyPressed = new ConsoleKeyInfo();
                        do
                        {
                            keyPressed = Console.ReadKey(true);
                            if (keyPressed.Key == ConsoleKey.Y)
                            {
                                switch (activeFlights)
                                {
                                    case FlightTypeEnum.Departure:
                                        if (departureFlights.GetLength(0) == 1)
                                            break;
                                        for (int index = activeDepartureIndex; index < departureFlights.GetLength(0) - 1; index++)
                                        {
                                            departureFlights[index] = departureFlights[index + 1];
                                        }
                                        Array.Resize(ref departureFlights, departureFlights.GetLength(0) - 1);
                                        if (departureFirstItemIndex > 0 && (departureFlights.GetLength(0) - departureFirstItemIndex) < panelFlightsQuantity)
                                            departureFirstItemIndex--;
                                        if (activeDepartureIndex == departureFlights.GetLength(0)) 
                                            activeDepartureIndex = departureFlights.GetLength(0) - 1;
                                        break;
                                    case FlightTypeEnum.Arrival:
                                        if (arrivalFlights.GetLength(0) == 1)
                                            break;
                                        for (int index = activeArrivalIndex; index < arrivalFlights.GetLength(0) - 1; index++)
                                        {
                                            arrivalFlights[index] = arrivalFlights[index + 1];
                                        }
                                        Array.Resize(ref arrivalFlights, arrivalFlights.GetLength(0) - 1);
                                        if (arrivalFirstItemIndex > 0 && (arrivalFlights.GetLength(0) - arrivalFirstItemIndex) < panelFlightsQuantity)
                                            arrivalFirstItemIndex--;
                                        if (activeArrivalIndex == arrivalFlights.GetLength(0)) 
                                            activeArrivalIndex = arrivalFlights.GetLength(0) - 1;
                                        break;
                                }
                            }
                        } while (keyPressed.Key != ConsoleKey.N &&
                                 keyPressed.Key != ConsoleKey.Y &&
                                 keyPressed.Key != ConsoleKey.Escape);
                        OutputToConsoleFromBufferArray(deleteDialogColumn, deleteDialogRow, 31, 3);
                        var borderNewRowPos = 0;
                        switch (activeFlights)
                        {
                            case FlightTypeEnum.Departure:
                                DisplayFlightsTable(departureFlights, 2, 5, departureFirstItemIndex, panelFlightsQuantity);
                                borderNewRowPos = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex);
                                DisplayOrMoveBorders(BorderTypeEnum.Double, 0, borderNewRowPos, rowLength, 1, true, 0, deleteDialogRow - 1);
                                break;
                            case FlightTypeEnum.Arrival:
                                DisplayFlightsTable(arrivalFlights, 131, 5, arrivalFirstItemIndex, panelFlightsQuantity);
                                borderNewRowPos = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex);
                                DisplayOrMoveBorders(BorderTypeEnum.Double, 129, borderNewRowPos, rowLength, 1, true, 129, deleteDialogRow - 1);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConsoleKey.F2:
                    {
                        var borderOldRowPos = 0;
                        var borderNewRowPos = 0;
                        switch (activeFlights)
                        {
                            case FlightTypeEnum.Departure:
                                Array.Resize(ref departureFlights, departureFlights.GetLength(0) + 1);
                                departureFlights[departureFlights.GetLength(0) - 1] = new string[] { "", "", "", DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), "", "", "CHECK-IN", "" };
                                borderOldRowPos = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex);
                                activeDepartureIndex = departureFlights.GetLength(0) - 1;
                                if (departureFlights.GetLength(0) > panelFlightsQuantity) departureFirstItemIndex = departureFlights.GetLength(0) - panelFlightsQuantity;
                                DisplayFlightsTable(departureFlights, 2, 5, departureFirstItemIndex, panelFlightsQuantity);
                                borderNewRowPos = 4 + 2 * (activeDepartureIndex - departureFirstItemIndex);
                                if (borderNewRowPos != borderOldRowPos) DisplayOrMoveBorders(BorderTypeEnum.Double, 0, borderNewRowPos, 120, 1, true, 0, borderOldRowPos);
                                EditFlight(departureFlights, activeDepartureIndex, 2, 5 + 2 * (activeDepartureIndex - departureFirstItemIndex));
                                DisplayFlightsTable(departureFlights, 2, 5, departureFirstItemIndex, panelFlightsQuantity);
                                break;
                            case FlightTypeEnum.Arrival:
                                Array.Resize(ref arrivalFlights, arrivalFlights.GetLength(0) + 1);
                                arrivalFlights[ arrivalFlights.GetLength(0) - 1] = new string[] { "", "", "", DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), "", "", "CHECK-IN", "" };
                                borderOldRowPos = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex);
                                activeArrivalIndex = arrivalFlights.GetLength(0) - 1;
                                if (arrivalFlights.GetLength(0) > panelFlightsQuantity) arrivalFirstItemIndex = arrivalFlights.GetLength(0) - panelFlightsQuantity;
                                DisplayFlightsTable(arrivalFlights, 131, 5, arrivalFirstItemIndex, panelFlightsQuantity);
                                borderNewRowPos = 4 + 2 * (activeArrivalIndex - arrivalFirstItemIndex);
                                if (borderNewRowPos != borderOldRowPos) DisplayOrMoveBorders(BorderTypeEnum.Double, 129, borderNewRowPos, 120, 1, true, 129, borderOldRowPos);
                                EditFlight(arrivalFlights, activeArrivalIndex, 131, 5 + 2 * (activeArrivalIndex - arrivalFirstItemIndex));
                                DisplayFlightsTable(arrivalFlights, 131, 5, arrivalFirstItemIndex, panelFlightsQuantity);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConsoleKey.F3:
                    {
                        var searchWindowResultRowsQuantity = 5;
                        SearchFlight(departureFlights, 0, FlightTypeEnum.Departure, searchWindowResultRowsQuantity);
                        OutputToConsoleFromBufferArray(65, 16, 124, 6 + searchWindowResultRowsQuantity * 2 - 1);
                    }
                    break;
                case ConsoleKey.F4:
                    {
                        var searchWindowResultRowsQuantity = 5;
                        SearchFlight(arrivalFlights, 0, FlightTypeEnum.Arrival);
                        OutputToConsoleFromBufferArray(65, 16, 124, 6 + searchWindowResultRowsQuantity * 2 - 1);
                    }
                    break;
                case ConsoleKey.Spacebar:
                    {
                        emergencyMessage = GetEmergencyInformation(emergencyMessage);
                        Console.CursorVisible = false;
                        OutputToConsoleFromBufferArray(26, 16, 202, 3);
                        OutputEmergencyMessage(emergencyMessage);
                    }
                    break;
                default:
                    break;
            }
        }
        while (input.Key != ConsoleKey.Escape);
    }

    public static void WriteToConsole(string text, bool writeToBuffer = true, ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.Gray)
    {
        Console.BackgroundColor = background;
        Console.ForegroundColor = foreground;
        var column = Console.CursorLeft;
        var row = Console.CursorTop;
        Console.Write(text);
        if (writeToBuffer)
        {
            foreach (var character in text)
            {
                consoleBufferChar[row][column] = character;
                consoleBufferColor[row][column] = (background, foreground);
                if (column < Console.BufferWidth - 1)
                {
                    column++;
                }
                else
                {
                    column = 0;
                    row++;
                }
            } 
        }
    }
    public static void OutputToConsoleFromBufferArray(int columnPosition = 0, int rowPosition = 0, int width = 0, int heigh = 0)
    {
        Console.SetCursorPosition(columnPosition, rowPosition);
        if (heigh == 0) heigh = consoleBufferChar.GetLength(0);
        if (width == 0) width = consoleBufferChar.GetLength(1);
        for (int i = rowPosition; i < rowPosition + heigh; i++)
        {
            Console.SetCursorPosition(columnPosition, i);
            for (int j = columnPosition; j < columnPosition + width; j++)
            {
                Console.BackgroundColor = consoleBufferColor[i][j].Item1;
                Console.ForegroundColor = consoleBufferColor[i][j].Item2;
                Console.Write(consoleBufferChar[i][j].ToString());
            }
        }
    }
    public static void StartFieldsOutput()
    {
        var top = "                                                  -= DEPARTURES =-                                                                                                         -= Arrivals =-                                                                   ";
        var topUnderline = "____________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________";
        var underTop = "  Terminal |Flight №   |City/Port            |Date Time        |Airline            |Gate |Status      |                 |         |Terminal |Flight №   |City/Port            |Date Time        |Airline            |Gate |Status      |                 |  ";
        var underTopUnderline = "============================================================================================================================================================================================================================================================";
        var downMenu = "                                                 Esc - EXIT           F2 - ADD FLIGHT            F3 - SEARCH(DEPARTURE)            F4 - SEARCH(ARRIVAL)             Enter - EDIT             Delete - DELETE                                               ";

        Console.SetCursorPosition(0, 0);
        WriteToConsole(top, true, ConsoleColor.Black, ConsoleColor.White);
        WriteToConsole(topUnderline, true, ConsoleColor.Black, ConsoleColor.Gray);
        WriteToConsole(underTop, true, ConsoleColor.Black, ConsoleColor.White);
        WriteToConsole(underTopUnderline, true, ConsoleColor.Black, ConsoleColor.Gray);
        Console.SetCursorPosition(0, 51);
        WriteToConsole(downMenu, true, ConsoleColor.Black, ConsoleColor.White);
    }
    public static T NextEnum<T>(T source)
    {
        if (typeof(T).IsEnum) new ArgumentException("Argument {0} is not an Enum", typeof(T).FullName);
        T[] array = (T[])Enum.GetValues(source.GetType());
        var index = Array.IndexOf<T>(array, source) + 1;

        return (index == array.Length) ? array[0] : array[index];
    }

    public static T PreviousEnum<T>(T source)
    {
        if (typeof(T).IsEnum) new ArgumentException("Argument [0] is not an Enum", typeof(T).FullName);
        T[] array = (T[])Enum.GetValues(source.GetType());
        var index = Array.IndexOf<T>(array, source) - 1;

        return (index < 0) ? array.LastOrDefault() : array[index];
    }

    public static void DisplayFlightsTable(string[][] flightsArray, int columnPos, int rowPos, int startItem, int itemsAmmount, bool writeToBuffer = true)
    {
        for (int i = startItem; i <  startItem + itemsAmmount; i++)
        {
            if (i >= flightsArray.GetLength(0))
            {
                Console.SetCursorPosition(columnPos, rowPos + (i - startItem) * 2);
                WriteToConsole(" ".PadRight(rowLength),writeToBuffer);
                continue;
            }
            var index = columnPos;
            for (int j = 0; j < flightsArray[i].Length; j++)
            {
                var foregroundColor = new ConsoleColor();
                switch (j)
                {
                    case (int)FieldsEnum.Terminal:
                        foregroundColor = ConsoleColor.Gray;
                        break;
                    case (int)FieldsEnum.FlightNumber:
                        foregroundColor = ConsoleColor.Yellow;
                        break;
                    case (int)FieldsEnum.CityPort:
                        foregroundColor = ConsoleColor.Gray;
                        break;
                    case (int)FieldsEnum.DateTimeOf:
                        foregroundColor = ConsoleColor.Gray;
                        break;
                    case (int)FieldsEnum.Airline:
                        foregroundColor = ConsoleColor.Gray;
                        break;
                    case (int)FieldsEnum.Gate:
                        foregroundColor = ConsoleColor.Yellow;
                        break;
                    case (int)FieldsEnum.FlightStatus:
                        switch (flightsArray[i][j])
                        {
                            case "CANCELED":
                            case "DELAYED":
                                foregroundColor = ConsoleColor.Red;
                                break;
                            default:
                                foregroundColor = ConsoleColor.Green;
                                break;
                        }
                        break;
                    case (int)FieldsEnum.TimeOfExpectance:
                        foregroundColor = ConsoleColor.Gray;
                        break;

                    default:
                        break;
                }

                Console.SetCursorPosition(index, rowPos + (i - startItem) * 2);
                WriteToConsole(flightsArray[i][j].PadRight(fieldsWidth[j]), writeToBuffer, ConsoleColor.Black, foregroundColor);
                index += fieldsWidth[j];
                Console.SetCursorPosition(index + 1, rowPos + (i - startItem) * 2);
                WriteToConsole("|", writeToBuffer, ConsoleColor.Black, ConsoleColor.Gray);
                index += 2;
            }
        }
    }
 
    public static void DisplayOrMoveBorders(BorderTypeEnum borderType, int columnPos, int rowPos, int width, int heigh, bool writeToBuffer = true, int boarderToDeleteStartColumn = 0, int boarderToDeleteStartRow = 0, bool fillInside = false)
    {

        var upperLeft = borderType == BorderTypeEnum.Double ? "╔" : "┌";
        var upperRight = borderType == BorderTypeEnum.Double ? "╗" : "┐";
        var horizontal = borderType == BorderTypeEnum.Double ? "═" : "─";
        var vertical = borderType == BorderTypeEnum.Double ? "║" : "│";
        var downLeft = borderType == BorderTypeEnum.Double ? "╚" : "└";
        var downRight = borderType == BorderTypeEnum.Double ? "╝" : "┘";

        if (boarderToDeleteStartColumn != 0 || boarderToDeleteStartRow != 0)
        {
            Console.SetCursorPosition(boarderToDeleteStartColumn, boarderToDeleteStartRow);
            WriteToConsole(" ".PadRight(width + 2), writeToBuffer);
            for (int i = 1; i <= heigh; i++)
            {
                Console.SetCursorPosition(boarderToDeleteStartColumn, boarderToDeleteStartRow + i);
                WriteToConsole(" ", writeToBuffer);
                Console.SetCursorPosition(boarderToDeleteStartColumn + width + 1, boarderToDeleteStartRow + i);
                WriteToConsole(" ", writeToBuffer);
            }
            Console.SetCursorPosition(boarderToDeleteStartColumn, boarderToDeleteStartRow + heigh + 1);
            WriteToConsole(" ".PadRight(width + 2), writeToBuffer);
        }

        Console.SetCursorPosition(columnPos, rowPos);
        WriteToConsole(upperLeft, writeToBuffer, foreground: ConsoleColor.White);
        for (int i = 1; i <= width; i++)
        {
            WriteToConsole(horizontal, writeToBuffer, foreground: ConsoleColor.White);
        }
        WriteToConsole(upperRight, writeToBuffer, foreground: ConsoleColor.White);

        for (int i = 1; i <= heigh; i++)
        {
            Console.SetCursorPosition(columnPos, rowPos + i);
            WriteToConsole(vertical, writeToBuffer, foreground: ConsoleColor.White);
            if (fillInside)
            {
                WriteToConsole(" ".PadRight(width), writeToBuffer);
            }
            else
                Console.SetCursorPosition(columnPos + width + 1, rowPos + i);
            WriteToConsole(vertical, writeToBuffer, foreground: ConsoleColor.White);
        }

        Console.SetCursorPosition(columnPos, rowPos + heigh + 1);
        WriteToConsole(downLeft, writeToBuffer, foreground: ConsoleColor.White);
        for (int i = 1; i <= width; i++)
        {
            WriteToConsole(horizontal, writeToBuffer, foreground: ConsoleColor.White);
        }
        WriteToConsole(downRight, writeToBuffer, foreground: ConsoleColor.White);
    }

    public static (ConsoleKey, string) EditField(int columnPosition, int rowPosition, int fieldMaxLength, FieldTypesEnum fieldWritingType, string[] possibleElements, string field = "")
    {
        var keyPressed = new ConsoleKeyInfo();

        switch (fieldWritingType)
        {
            case FieldTypesEnum.Number:
                (field, keyPressed) = GetNumberField(columnPosition, rowPosition, field, fieldMaxLength);
                break;
            case FieldTypesEnum.DateAndTime:
                (field, keyPressed) = GetDateAndTimeField(columnPosition, rowPosition, field);
                break;
            case FieldTypesEnum.Upercase:
                (field, keyPressed) = GetUpercaseField(columnPosition, rowPosition, field, fieldMaxLength);
                break;
            case FieldTypesEnum.Mixedcase:
                (field, keyPressed) = GetMixedcaseField(columnPosition, rowPosition, field, fieldMaxLength, possibleElements);
                break;
            case FieldTypesEnum.ListOfElements:
                (field, keyPressed) = GetListOfElementsField(columnPosition, rowPosition, field, fieldMaxLength, possibleElements);
                break;
            case FieldTypesEnum.TrueOrFalse:
                {
                    Console.SetCursorPosition(columnPosition, rowPosition);
                    Console.CursorVisible = true;
                    do
                    {
                        keyPressed = Console.ReadKey(true);
                        if (keyPressed.Key == ConsoleKey.Spacebar)
                        {
                            Console.SetCursorPosition(columnPosition, rowPosition);
                            if (field == "")
                            {
                                field = "X";
                                Console.Write(field);
                            }
                            else
                            {
                                field = "";
                                Console.Write(whiteSpace);
                            }
                            Console.SetCursorPosition(columnPosition, rowPosition);
                        }
                    } while (keyPressed.Key != ConsoleKey.Enter &&
                             keyPressed.Key != ConsoleKey.LeftArrow &&
                             keyPressed.Key != ConsoleKey.RightArrow &&
                             keyPressed.Key != ConsoleKey.Tab &&
                             keyPressed.Key != ConsoleKey.Escape &&
                             keyPressed.Key != ConsoleKey.UpArrow &&
                             keyPressed.Key != ConsoleKey.DownArrow);
                }
                break;
        }

        Console.CursorVisible = false;

        return (keyPressed.Key, field);
    }

    private static (string, ConsoleKeyInfo) GetNumberField(int columnPosition, int rowPosition, string field, int fieldMaxLength)
    {
        var keyPressed = new ConsoleKeyInfo();
        var cursorX = 0;
        var fieldNewValue = field;

        if (fieldNewValue.Length == fieldMaxLength)
        {
            cursorX = columnPosition + fieldNewValue.Length - 1;
        }
        else
            cursorX = columnPosition + fieldNewValue.Length;
        Console.SetCursorPosition(cursorX, rowPosition);
        Console.CursorVisible = true;

        do
        {
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Modifiers == ConsoleModifiers.Alt || 
                keyPressed.Modifiers == ConsoleModifiers.Control ||
                keyPressed.Modifiers == ConsoleModifiers.Shift)
                continue;
            if (keyPressed.Key == ConsoleKey.Backspace && fieldNewValue.Length != 0)
            {
                if (cursorX - columnPosition != fieldNewValue.Length - 1)
                {
                    cursorX--;
                    Console.SetCursorPosition(cursorX, rowPosition);
                }
                fieldNewValue = fieldNewValue.Remove(fieldNewValue.Length - 1);
                Console.Write(whiteSpace);
                Console.SetCursorPosition(cursorX, rowPosition);
                continue;
            }
            if (fieldNewValue.Length == fieldMaxLength)
                continue;
            if (Enumerable.Range(48, 10).Contains((int)keyPressed.Key) ||
                Enumerable.Range(96, 10).Contains((int)keyPressed.Key))
            {
                Console.Write(keyPressed.KeyChar.ToString());
                fieldNewValue += keyPressed.KeyChar.ToString();
                if (fieldNewValue.Length == fieldMaxLength)
                {
                    Console.SetCursorPosition(cursorX, rowPosition);
                    continue;
                }
                cursorX++;
            }
        }
        while (keyPressed.Key != ConsoleKey.Enter &&
               keyPressed.Key != ConsoleKey.LeftArrow &&
               keyPressed.Key != ConsoleKey.RightArrow &&
               keyPressed.Key != ConsoleKey.Tab &&
               keyPressed.Key != ConsoleKey.Escape);

        if (keyPressed.Key == ConsoleKey.Escape)
            return (field, keyPressed);

        return (fieldNewValue, keyPressed);
    }
    public static (string, ConsoleKeyInfo) GetDateAndTimeField(int columnPosition, int rowPosition, string field)
    {
        var fieldEditionDone = false;
        var activeDateTimePosition = DateTimePositionOffsetEnum.Day;
        var date = DateTime.Parse(field);
        var cursorX = 0;
        var keyPressed = new ConsoleKeyInfo();

        cursorX = columnPosition + (int) activeDateTimePosition;
        Console.SetCursorPosition(cursorX, rowPosition);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.Write(field.Substring((int) activeDateTimePosition, 2));

        do
        {
            keyPressed = Console.ReadKey(true);
            switch (keyPressed.Key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                    {
                        var positiveOrNegative = 1;
                        if (keyPressed.Key == ConsoleKey.DownArrow)
                            positiveOrNegative = -1;
                        switch (activeDateTimePosition)
                        {
                            case DateTimePositionOffsetEnum.Day:
                                date = date.AddDays(positiveOrNegative);
                                break;
                            case DateTimePositionOffsetEnum.Month:
                                date = date.AddMonths(positiveOrNegative);
                                break;
                            case DateTimePositionOffsetEnum.Year:
                                date = date.AddYears(positiveOrNegative);
                                break;
                            case DateTimePositionOffsetEnum.Hour:
                                date = date.AddHours(positiveOrNegative);
                                break;
                            case DateTimePositionOffsetEnum.Mitutes:
                                date = date.AddMinutes(positiveOrNegative);
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (activeDateTimePosition == DateTimePositionOffsetEnum.Day)
                    {
                        fieldEditionDone = true;
                        break;
                    }
                    activeDateTimePosition = PreviousEnum<DateTimePositionOffsetEnum>(activeDateTimePosition);
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.Tab:
                    if (activeDateTimePosition == DateTimePositionOffsetEnum.Mitutes)
                    {
                        fieldEditionDone = true;
                        break;
                    }
                    activeDateTimePosition = NextEnum<DateTimePositionOffsetEnum>(activeDateTimePosition);
                    break;
                case ConsoleKey.Enter:
                    fieldEditionDone = true;
                    break;
                case ConsoleKey.Escape:
                    {
                        fieldEditionDone = true;
                        date = DateTime.Parse(field);
                    }
                    break;
                default:
                    break;
            }
            DisplayDateTimeField(date.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), activeDateTimePosition, columnPosition, rowPosition);
        }
        while (!fieldEditionDone);
        Console.ResetColor();
        Console.SetCursorPosition(columnPosition, rowPosition);
        Console.Write(date.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
        return (date.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), keyPressed);
    }

    private static void DisplayDateTimeField(string date, DateTimePositionOffsetEnum activeDateTimePosition, int columnPosition, int rowPosition)
    {
        if (activeDateTimePosition != DateTimePositionOffsetEnum.Day)
        {
            Console.ResetColor();
            Console.SetCursorPosition(columnPosition, rowPosition);
            Console.Write(date.Substring(0, ((int)activeDateTimePosition)));
        }

        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(columnPosition + ((int)activeDateTimePosition), rowPosition);
        var activeDateTimePositionLength = 0;
        if (activeDateTimePosition == DateTimePositionOffsetEnum.Year)
            activeDateTimePositionLength = 4;
        else
            activeDateTimePositionLength = 2;
        Console.Write(date.Substring(((int)activeDateTimePosition), activeDateTimePositionLength));

        if (activeDateTimePosition != DateTimePositionOffsetEnum.Mitutes)
        {
            Console.ResetColor();
            Console.SetCursorPosition(columnPosition + (int)(NextEnum<DateTimePositionOffsetEnum>(activeDateTimePosition)), rowPosition);
            Console.Write(date.Substring((int)(NextEnum<DateTimePositionOffsetEnum>(activeDateTimePosition))));
        }
    }
    private static (string, ConsoleKeyInfo) GetUpercaseField(int columnPosition, int rowPosition, string field, int fieldMaxLength)
    {
        var keyPressed = new ConsoleKeyInfo();
        var fieldNewValue = field;
        var cursorX = 0;

        if (fieldNewValue.Length == fieldMaxLength)
        {
            cursorX = columnPosition + fieldNewValue.Length - 1;
        }
        else
            cursorX = columnPosition + fieldNewValue.Length;

        Console.SetCursorPosition(cursorX, rowPosition);
        Console.CursorVisible = true;

        do
        {
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Modifiers == ConsoleModifiers.Alt || keyPressed.Modifiers == ConsoleModifiers.Control)
                continue;
            if (keyPressed.Key == ConsoleKey.Backspace && fieldNewValue.Length != 0)
            {
                if (cursorX - columnPosition != fieldNewValue.Length - 1)
                {
                    cursorX--;
                    Console.SetCursorPosition(cursorX, rowPosition);
                }
                fieldNewValue = fieldNewValue.Remove(fieldNewValue.Length - 1);
                Console.Write(whiteSpace);
                Console.SetCursorPosition(cursorX, rowPosition);
                continue;
            }
            if (fieldNewValue.Length == fieldMaxLength)
                continue;
            if (Enumerable.Range(48, 10).Contains((int)keyPressed.Key) ||
                Enumerable.Range(96, 10).Contains((int)keyPressed.Key) ||
                Enumerable.Range(65, 26).Contains((int)keyPressed.Key))
            {
                Console.Write(char.ToUpper(keyPressed.KeyChar.ToString()[0]));
                fieldNewValue += char.ToUpper(keyPressed.KeyChar.ToString()[0]);
                if (fieldNewValue.Length == fieldMaxLength)
                {
                    Console.SetCursorPosition(cursorX, rowPosition);
                continue;
                }
                cursorX++;
            }
        }
        while (keyPressed.Key != ConsoleKey.Enter &&
               keyPressed.Key != ConsoleKey.LeftArrow &&
               keyPressed.Key != ConsoleKey.RightArrow &&
               keyPressed.Key != ConsoleKey.Tab &&
               keyPressed.Key != ConsoleKey.Escape &&
               keyPressed.Key != ConsoleKey.DownArrow &&
               keyPressed.Key != ConsoleKey.UpArrow);

        if (keyPressed.Key == ConsoleKey.Escape)
            return (field, keyPressed);
        return (fieldNewValue, keyPressed);
    }
    private static (string, ConsoleKeyInfo) GetMixedcaseField(int columnPosition, int rowPosition, string field, int fieldMaxLength, string[] possibleElements)
    {
        var keyPressed = new ConsoleKeyInfo();
        var fieldNewValue = field;
        var cursorX = 0;

        if (fieldNewValue.Length == fieldMaxLength)
        {
            cursorX = columnPosition + fieldNewValue.Length - 1;
        }
        else
            cursorX = columnPosition + fieldNewValue.Length;

        Console.SetCursorPosition(cursorX, rowPosition);
        Console.CursorVisible = true;

        do
        {
            keyPressed = Console.ReadKey(true);
            if (keyPressed.Modifiers == ConsoleModifiers.Alt || keyPressed.Modifiers == ConsoleModifiers.Control)
                continue;
            if (keyPressed.Key == ConsoleKey.Backspace && fieldNewValue.Length != 0)
            {
                if (cursorX - columnPosition != fieldNewValue.Length - 1)
                {
                    cursorX--;
                    Console.SetCursorPosition(cursorX, rowPosition);
                }
                fieldNewValue = fieldNewValue.Remove(fieldNewValue.Length - 1);
                Console.Write(whiteSpace);
                Console.SetCursorPosition(cursorX, rowPosition);
                continue;
            }
            if (fieldNewValue.Length == fieldMaxLength)
                continue;
            if (Enumerable.Range(48, 10).Contains((int)keyPressed.Key) ||
                Enumerable.Range(96, 10).Contains((int)keyPressed.Key) ||
                Enumerable.Range(65, 26).Contains((int)keyPressed.Key))
            {
                if (keyPressed.Modifiers == ConsoleModifiers.Shift)
                {
                    Console.Write(keyPressed.KeyChar.ToString());
                    fieldNewValue += keyPressed.KeyChar.ToString();
                }
                else
                {
                    Console.Write(char.ToLower(keyPressed.KeyChar.ToString()[0]));
                    fieldNewValue += char.ToLower(keyPressed.KeyChar.ToString()[0]);
                }
                if (fieldNewValue.Length == fieldMaxLength)
                {
                    Console.SetCursorPosition(cursorX, rowPosition);
                    continue;
                }
                cursorX++;
            }
            if (keyPressed.Key == ConsoleKey.Spacebar)
            {
                    Console.Write(whiteSpace);
                    fieldNewValue += " ";
                    if (fieldNewValue.Length == fieldMaxLength)
                    {
                        Console.SetCursorPosition(cursorX, rowPosition);
                    continue;
                    }
                    cursorX++;
            }
         }
        while (keyPressed.Key != ConsoleKey.Enter &&
               keyPressed.Key != ConsoleKey.LeftArrow &&
               keyPressed.Key != ConsoleKey.RightArrow &&
               keyPressed.Key != ConsoleKey.Tab &&
               keyPressed.Key != ConsoleKey.Escape &&
               keyPressed.Key != ConsoleKey.DownArrow &&
               keyPressed.Key != ConsoleKey.UpArrow);

        return (fieldNewValue, keyPressed);
    }
    private static (string, ConsoleKeyInfo) GetListOfElementsField(int columnPosition, int rowPosition, string field, int fieldMaxLength, string[] possibleElements)
    {
        var keyPressed = new ConsoleKeyInfo();
        var activeElement = Array.IndexOf(possibleElements, field);

        if (activeElement < 0)
            activeElement = 0;

        DisplayOrMoveBorders(BorderTypeEnum.Single, columnPosition - 1, rowPosition + 2, fieldMaxLength, possibleElements.Length, false);

        Console.SetCursorPosition(columnPosition, rowPosition);
        Console.CursorVisible = false;
        Console.ForegroundColor = (field == "CANCELED" || field == "DELAYED") ? ConsoleColor.Red : ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write(possibleElements[activeElement].PadRight(fieldMaxLength));
        for (int index = 0; index < possibleElements.Length; index++)
        {
            Console.SetCursorPosition(columnPosition, rowPosition + 3 + index);
            Console.ForegroundColor = (possibleElements[index] == "CANCELED" || possibleElements[index] == "DELAYED") ?
                ConsoleColor.Red :
                ConsoleColor.Green;
            if (index == activeElement)
                Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(possibleElements[index].PadRight(fieldMaxLength));
            Console.BackgroundColor = ConsoleColor.Black;
        }
        do
        {
            keyPressed = Console.ReadKey();
            switch (keyPressed.Key)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    field = possibleElements[activeElement];
                    Console.ForegroundColor = (field == "CANCELED" || field == "DELAYED") ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.SetCursorPosition(columnPosition, rowPosition);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(field.PadRight(fieldMaxLength));
                    break;
                case ConsoleKey.UpArrow:
                    if (activeElement > 0)
                    {
                        Console.SetCursorPosition(columnPosition, rowPosition + 3 + activeElement);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = (possibleElements[activeElement] == "CANCELED" || possibleElements[activeElement] == "DELAYED") ?
                            ConsoleColor.Red :
                            ConsoleColor.Green;
                        Console.Write(possibleElements[activeElement].PadRight(fieldMaxLength));
                        activeElement--;
                        Console.SetCursorPosition(columnPosition, rowPosition + 3 + activeElement);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = (possibleElements[activeElement] == "CANCELED" || possibleElements[activeElement] == "DELAYED") ?
                            ConsoleColor.Red :
                            ConsoleColor.Green;
                        Console.Write(possibleElements[activeElement].PadRight(fieldMaxLength));
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (activeElement < possibleElements.Length - 1)
                    {
                        Console.SetCursorPosition(columnPosition, rowPosition + 3 + activeElement);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = (possibleElements[activeElement] == "CANCELED" || possibleElements[activeElement] == "DELAYED") ?
                           ConsoleColor.Red :
                           ConsoleColor.Green;
                        Console.Write(possibleElements[activeElement].PadRight(fieldMaxLength));
                        activeElement++;
                        Console.SetCursorPosition(columnPosition, rowPosition + 3 + activeElement);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = (possibleElements[activeElement] == "CANCELED" || possibleElements[activeElement] == "DELAYED") ?
                            ConsoleColor.Red :
                            ConsoleColor.Green;
                        Console.Write(possibleElements[activeElement].PadRight(fieldMaxLength));
                    }
                    break;
                default:
                    break;
            }

        } while (keyPressed.Key != ConsoleKey.LeftArrow &&
                 keyPressed.Key != ConsoleKey.RightArrow &&
                 keyPressed.Key != ConsoleKey.Tab &&
                 keyPressed.Key != ConsoleKey.Escape);

        return (possibleElements[activeElement], keyPressed);
    }

    public static void EditFlight(string[][] flightsArray, int activeFlight, int itemColumnPos, int itemRowPos)
    {
        var keyPressed = new ConsoleKey();
        var columnPos = itemColumnPos;
        var rowPos = itemRowPos;
        var activeField = FieldsEnum.Terminal;
        var fieldWritingType = FieldTypesEnum.Upercase;
        var possibleElements = Array.Empty<string>();

        do
        {
            (keyPressed, flightsArray[activeFlight][(int)activeField]) = EditField(columnPos, rowPos, fieldsWidth[(int)activeField], fieldWritingType, possibleElements, flightsArray[activeFlight][(int)activeField]);

            if (activeField == FieldsEnum.FlightStatus)
            {
                OutputToConsoleFromBufferArray(columnPos - 1, rowPos + 2, fieldsWidth[(int)activeField] + 2, possibleElements.Length + 2);
                if (new string[] {"DEPARTED AT", "EXPECTED AT"}.Contains(flightsArray[activeFlight][(int)FieldsEnum.FlightStatus]))
                {
                    if (flightsArray[activeFlight][(int)FieldsEnum.TimeOfExpectance] == "")
                        flightsArray[activeFlight][(int)FieldsEnum.TimeOfExpectance] = DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                else
                    flightsArray[activeFlight][(int)FieldsEnum.TimeOfExpectance] = "";
                Console.SetCursorPosition(columnPos + fieldsWidth[((int)activeField)] + 2, rowPos);
                Console.ResetColor();
                Console.Write(flightsArray[activeFlight][(int)FieldsEnum.TimeOfExpectance].PadRight(fieldsWidth[(int)FieldsEnum.TimeOfExpectance]));
            }

            if (keyPressed == ConsoleKey.Enter)
                break;
            if (keyPressed == ConsoleKey.Escape)
                break;
            if (keyPressed != ConsoleKey.LeftArrow && keyPressed != ConsoleKey.RightArrow && keyPressed != ConsoleKey.Tab)
                continue;
            if (keyPressed == ConsoleKey.LeftArrow && activeField != FieldsEnum.Terminal)
            {
                    activeField--;
                    columnPos -= fieldsWidth[(int)activeField] + 2;
            }

            if ((keyPressed == ConsoleKey.RightArrow || keyPressed == ConsoleKey.Tab) && 
                activeField != FieldsEnum.TimeOfExpectance)
            {
                if ((activeField == FieldsEnum.FlightStatus && flightsArray[activeFlight][((int)FieldsEnum.TimeOfExpectance)] != "") ||
                    (activeField < FieldsEnum.FlightStatus))
                {
                    columnPos += fieldsWidth[((int)activeField)] + 2;
                    activeField++;
                }
            }

            switch (activeField)
            {
                case FieldsEnum.Terminal:
                case FieldsEnum.FlightNumber:
                    {
                        fieldWritingType = FieldTypesEnum.Upercase;
                        possibleElements = Array.Empty<string>();
                    }
                    break;
                case FieldsEnum.CityPort:
                case FieldsEnum.Airline:
                    {
                        fieldWritingType = FieldTypesEnum.Mixedcase;
                        Array.Resize<string>(ref possibleElements, 0);
                        possibleElements.Append(flightsArray[0][(int)activeField]);
                        for (int i = 1; i < flightsArray.GetLength(0); i++)
                        {
                            if (!possibleElements.Contains(flightsArray[i][(int)activeField]))
                            {
                                possibleElements.Append(flightsArray[i][(int)activeFlight]);
                            }
                        }
                    }
                    break;
                case FieldsEnum.Gate:
                    {
                        fieldWritingType = FieldTypesEnum.Number;
                        possibleElements = Array.Empty<string>();
                    }
                    break;
                case FieldsEnum.FlightStatus:
                    {
                        fieldWritingType = FieldTypesEnum.ListOfElements;
                        Array.Resize<string>(ref possibleElements, flightStatuses.Length);
                        Array.Copy(flightStatuses, possibleElements, flightStatuses.Length);
                    }
                    break;
                case FieldsEnum.DateTimeOf:
                case FieldsEnum.TimeOfExpectance:
                    {
                        fieldWritingType = FieldTypesEnum.DateAndTime;
                        possibleElements = Array.Empty<string>();
                    }
                    break;
                default:
                    break;
            }
        }
        while (true);
    }
    public static void SearchFlight(string[][] flightsArray, int activeFlight, FlightTypeEnum flightsType, int searchResultWindowRowsQuantity = 5)
    {
        DisplaySearchWindow(flightsType, searchResultWindowRowsQuantity);

        var searchFieldsPosition = new int[] { 78, 102, 132, 186 };
        var searchFielrsWidth = new int[] { 10, 20, 16, 1 };
        var searchResultArray = new string[][] { };
        var searchFieldsValue = new string[] { "", "", DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), "" };
        Console.ResetColor();
        Console.SetCursorPosition(searchFieldsPosition[((int)SearchFieldEnum.DateTimeOf)], 19);
        Console.Write(searchFieldsValue[((int)SearchFieldEnum.DateTimeOf)]);
        var startFlightToDisplay = 0;
        var keyPressed = new ConsoleKey();
        var dateTimeOfDepartureArrival = DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        var fieldValue = string.Empty;
        var activeField = SearchFieldEnum.FlightNumber;
        var activeFieldTypeArray = new FieldTypesEnum[] { FieldTypesEnum.Upercase, FieldTypesEnum.Mixedcase, FieldTypesEnum.DateAndTime, FieldTypesEnum.TrueOrFalse };
        var searchNearest = false;

        do
        {
            (keyPressed, fieldValue) = EditField(searchFieldsPosition[((int)activeField)], 19, searchFielrsWidth[(int)activeField], activeFieldTypeArray[((int)activeField)], new string[] { }, searchFieldsValue[((int)activeField)]);
            if (keyPressed == ConsoleKey.Escape)
                break;
            searchFieldsValue[((int)activeField)] = fieldValue;
            if (searchFieldsValue[((int)SearchFieldEnum.SearchNearest)] == "" && searchNearest)
                searchNearest = false;
            if (searchFieldsValue[((int)SearchFieldEnum.SearchNearest)] != "" && !searchNearest)
                searchNearest = true;

            searchResultArray = GetSearchedFlights(flightsArray, searchFieldsValue[((int)SearchFieldEnum.FlightNumber)], searchFieldsValue[((int)SearchFieldEnum.CityPort)], searchFieldsValue[((int)SearchFieldEnum.DateTimeOf)], searchNearest);
            switch (keyPressed)
            {
                case ConsoleKey.LeftArrow:
                    if (activeField != SearchFieldEnum.FlightNumber)
                        activeField--;
                    break;
                case ConsoleKey.RightArrow:
                    if (activeField != SearchFieldEnum.SearchNearest)
                        activeField++;
                    break;
                case ConsoleKey.UpArrow:
                    if (startFlightToDisplay > 0)
                        startFlightToDisplay--;
                    break;
                case ConsoleKey.DownArrow:
                    if (searchResultArray.GetLength(0) - startFlightToDisplay > searchResultWindowRowsQuantity)
                        startFlightToDisplay++;
                    break;
                default:
                    break;
            }
            DisplayFlightsTable(searchResultArray, 66, 21, startFlightToDisplay, searchResultWindowRowsQuantity, false);
        } while (true);

    }

    private static string[][] GetSearchedFlights(string[][] flightsArray, string flightNumber, string cityPort, string dateTimeOfFlight, bool searchNearest)
    {
        var searchedArray = new string[][] { };
        flightNumber = flightNumber.Trim();
        cityPort = cityPort.Trim();
        foreach (var flight in flightsArray)
        {
            if (flightNumber != "" && !flight[((int)FieldsEnum.FlightNumber)].StartsWith(flightNumber))
                continue;
            if (cityPort != "" && !flight[((int)FieldsEnum.CityPort)].StartsWith(cityPort))
                continue;
            switch (searchNearest)
            {
                case false:
                    if (dateTimeOfFlight == "" || dateTimeOfFlight != flight[(int)FieldsEnum.DateTimeOf])
                        continue;
                    break;
                case true:
                    if (dateTimeOfFlight == "")
                        continue;
                    var dateOfFlight = DateTime.Parse(flight[((int)FieldsEnum.DateTimeOf)]);
                    var expectedDate = DateTime.Parse(dateTimeOfFlight);
                    if (Math.Abs((dateOfFlight - expectedDate).Hours) > 0)
                        continue;
                    break;
            }
            Array.Resize(ref searchedArray, searchedArray.GetLength(0) + 1);
            searchedArray[searchedArray.GetLength(0) - 1] = flight;
        }
        return searchedArray;
    }

    private static void DisplaySearchWindow(FlightTypeEnum flightsType, int searchResultWindowRowsQuantity)
    {
        DisplayOrMoveBorders(BorderTypeEnum.Double, 65, 16, 122, 4 + searchResultWindowRowsQuantity * 2 - 1, false, fillInside: true);
        DisplayOrMoveBorders(BorderTypeEnum.Single, 65, 20, 122, searchResultWindowRowsQuantity * 2 - 1, false);
        DisplayOrMoveBorders(BorderTypeEnum.Double, 65, 18, 122, 2 + searchResultWindowRowsQuantity * 2 - 1, false);
        DisplayOrMoveBorders(BorderTypeEnum.Double, 65, 16, 122, 4 + searchResultWindowRowsQuantity * 2 - 1, false);
        Console.SetCursorPosition(66, 17);
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Black;
        switch (flightsType)
        {
            case FlightTypeEnum.Departure:
                Console.Write("                                    S E A R C H       D E P A R T U R E S       B Y :                                     ");
                break;
            case FlightTypeEnum.Arrival:
                Console.Write("                                      S E A R C H       A R R I V A L S       B Y :                                       ");
                break;
        }
        Console.SetCursorPosition(66, 19);
        Console.Write(" Flight № :");
        Console.SetCursorPosition(87, 19);
        Console.Write(" City (Port) :");
        Console.SetCursorPosition(111, 19);
        Console.Write(" Date of Departure :");
        Console.SetCursorPosition(150, 19);
        Console.Write(" Search All (1 Hour Before/After) :");
    }
    private static string GetEmergencyInformation(string emergencyMessage)
    {
        var emergenceMessageMaxLength = Console.WindowWidth * 2;
        var emergencyTemporaryMessage = emergencyMessage;
        DisplayOrMoveBorders(BorderTypeEnum.Double, 26, 16, 200, 1, false, fillInside: true);
        Console.SetCursorPosition(27, 17);
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write("Emergency Message:");
        var startPosition = 0;
        var cursorPosition = 0;
        Console.SetCursorPosition(46, 17);
        Console.ResetColor();
        Console.Write(emergencyTemporaryMessage.PadRight(startPosition + 181).Substring(startPosition, 180));
        Console.CursorVisible = true;
        Console.SetCursorPosition(46, 17);

        var keyPressed = new ConsoleKeyInfo();
        var enteredSymbol = whiteSpace;
        do
        {
            keyPressed = Console.ReadKey(true);
            if ((keyPressed.Modifiers == ConsoleModifiers.Alt || keyPressed.Modifiers == ConsoleModifiers.Control))
                continue;
            switch (keyPressed.Key)
            {
                case ConsoleKey.Escape:
                    return emergencyMessage;
                case ConsoleKey.Enter:
                    return emergencyTemporaryMessage;
                case ConsoleKey.LeftArrow:
                    if (cursorPosition > 0)
                        cursorPosition--;
                    if (startPosition > cursorPosition)
                        startPosition = cursorPosition;
                    break;
                case ConsoleKey.RightArrow:
                    if (cursorPosition == emergenceMessageMaxLength - 1)
                        break;
                    if (cursorPosition == emergencyTemporaryMessage.Length)
                        break;
                    cursorPosition++;
                    if (cursorPosition - startPosition > 180)
                        startPosition++;
                    break;
                case ConsoleKey.Home:
                    cursorPosition = 0;
                    startPosition = 0;
                    break;
                case ConsoleKey.End:
                    if (emergencyTemporaryMessage.Length == emergenceMessageMaxLength)
                        cursorPosition = emergencyTemporaryMessage.Length - 1;
                    else
                        cursorPosition = emergencyTemporaryMessage.Length;
                    if (cursorPosition - startPosition > 180)
                        startPosition = cursorPosition - 180;
                    break;
                case ConsoleKey.Backspace:
                    if (cursorPosition == 0)
                        break;
                    if (cursorPosition != emergenceMessageMaxLength)
                        cursorPosition--;
                    emergencyTemporaryMessage = emergencyTemporaryMessage.Remove(cursorPosition, 1);
                    if (startPosition > cursorPosition)
                        startPosition = cursorPosition;
                    break;
                case ConsoleKey.Delete:
                    if (cursorPosition > emergencyTemporaryMessage.Length - 1)
                        break;
                    emergencyTemporaryMessage = emergencyTemporaryMessage.Remove(cursorPosition, 1);
                    break;
            }

            if ((Enumerable.Range(48, 10).Contains((int)keyPressed.Key)) ||
                (Enumerable.Range(96, 10).Contains((int)keyPressed.Key)) ||
                (Enumerable.Range(65, 26).Contains((int)keyPressed.Key)) ||
                (keyPressed.Key == ConsoleKey.Spacebar))
            {
                if (emergencyTemporaryMessage.Length == emergenceMessageMaxLength)
                    continue;
                enteredSymbol = keyPressed.KeyChar.ToString()[0];
                if (cursorPosition < emergencyTemporaryMessage.Length)
                    emergencyTemporaryMessage = emergencyTemporaryMessage.Insert(cursorPosition, enteredSymbol.ToString());
                else
                    emergencyTemporaryMessage += enteredSymbol.ToString();
                if (emergencyTemporaryMessage.Length != emergenceMessageMaxLength)
                    cursorPosition++;
                if (cursorPosition - startPosition > 180)
                    startPosition++;
            }

            Console.CursorVisible = false;
            Console.SetCursorPosition(46, 17);
            Console.Write(emergencyTemporaryMessage.PadRight(startPosition + 182).Substring(startPosition, 181));
            Console.SetCursorPosition(46 + (cursorPosition - startPosition), 17);
            Console.CursorVisible = true;
        } while (true);
    }
    private static void OutputEmergencyMessage(string emergencyMessage)
    {
        Console.SetCursorPosition(0, 46);
        WriteToConsole("!!!  A T T E N T I O N  !!!", background: ConsoleColor.White, foreground: ConsoleColor.Black);
        Console.SetCursorPosition(0, 48);
        WriteToConsole(emergencyMessage, background: ConsoleColor.White, foreground: ConsoleColor.Black);
        WriteToConsole("".PadRight(504 - emergencyMessage.Length));
        Console.ResetColor();
    }
}
