using System;
using System.Data;

namespace DataSets
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DB("Data Source=.\\;Initial Catalog=DataSetsDB;Integrated Security=True"))
            {
                Console.WriteLine("SELECT *:");
                printEmailValues(db);
                Console.WriteLine();

                Console.WriteLine("After INSERT:");
                db.InsertIntoEmailTable();
                printEmailValues(db);
                Console.WriteLine();

                Console.WriteLine("After UPDATE:");
                db.UpdateEmailTable();
                printEmailValues(db);
                Console.WriteLine();

                Console.WriteLine("After DELETE:");
                db.DeleteFromEmailTable();
                printEmailValues(db);
                Console.WriteLine();
            }
        }

        private static void printEmailValues(DB db)
        {
            var sqlReader = db.ReadEmailTable();

            while (sqlReader.Read())
            {
                readSingleRow(sqlReader);
            }

            sqlReader.Close();
        }
        private static void readSingleRow(IDataRecord record)
        {
            Console.WriteLine($"email id: {record[0]}, email: {record[1]}, lecturer id: {record[2]}");
        }
    }
}
