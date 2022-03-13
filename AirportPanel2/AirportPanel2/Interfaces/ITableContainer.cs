using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportPanel2
{
    public interface ITableContainer
    {
        public int Length { get; }
        public int TableWidth { get; }
        public string TableName { get; }
        public Airline Airline { get; } 
        public void PrintTableTitle(ConsoleColor borderColor);
        public void PrintTableRow(int rowIndex, ConsoleColor borderColor);
        public void WorkWithMenu();
    }
}
