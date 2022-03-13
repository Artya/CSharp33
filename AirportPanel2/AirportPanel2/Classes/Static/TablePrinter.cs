using System;

namespace AirportPanel2
{
    public static class TablePrinter
    {
        public static void PrintTable(ITableContainer tableContainer, ConsoleColor tableBorderColor)
        {
            if (tableContainer.TableWidth > Console.WindowWidth && tableContainer.TableWidth < Console.LargestWindowWidth)
            {
                Console.WindowWidth = tableContainer.TableWidth;
                Console.BufferWidth = tableContainer.TableWidth;
            }

            Console.WriteLine(tableContainer.TableName);
            WriteHorisontalBorder(tableContainer.TableWidth, '=', tableBorderColor);
            WriteVerticalBorder(tableBorderColor);
            tableContainer.PrintTableTitle(tableBorderColor);
            WriteHorisontalBorder(tableContainer.TableWidth, '=', tableBorderColor);

            for (var i = 0; i < tableContainer.Length; i++)
            {
                if (i > 0)
                    WriteHorisontalBorder(tableContainer.TableWidth, '-', tableBorderColor);

                WriteVerticalBorder(tableBorderColor);
                tableContainer.PrintTableRow(i, tableBorderColor);
            }

            WriteHorisontalBorder(tableContainer.TableWidth, '=', tableBorderColor);
            WriteVerticalBorder(tableBorderColor);
            WriteCell($"Total {tableContainer.TableName} : {tableContainer.Length}", tableContainer.TableWidth - (2 * Constants.BorderWidth), tableBorderColor, CellTextLevelling.Right);
            Console.WriteLine();
            WriteHorisontalBorder(tableContainer.TableWidth, '=', tableBorderColor);
        }

        public static void WriteHorisontalBorder(int tableWidth, char symbol, ConsoleColor tableBorderColor)
        {
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

        public static void WriteCell(string value, int columnWidth, ConsoleColor borderColor, CellTextLevelling textLevelling = CellTextLevelling.Center)
        {
            value = value.Trim();

            var cellText = string.Empty;

            switch (textLevelling)
            {
                case CellTextLevelling.Center:

                    var offset = (int)((columnWidth - value.Length) / 2);
                    var spaceCount = columnWidth - value.Length - offset;
                    cellText += new string(' ', offset);
                    cellText += value;
                    
                    if(spaceCount >= 0)
                        cellText +=  new string(' ', spaceCount);

                    break;

                case CellTextLevelling.Left:

                    var rightSpaceCount = columnWidth - value.Length;
                    cellText += value;
                    cellText += new string(' ', rightSpaceCount);

                    break;

                case CellTextLevelling.Right:

                    var leftSpaceCount = columnWidth - value.Length;

                    cellText += new string(' ', leftSpaceCount);
                    cellText += value;

                    break;
            }

            Console.Write(cellText);                       
            WriteVerticalBorder(borderColor);
        }
    }
}
