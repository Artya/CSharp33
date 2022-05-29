using System;

namespace DataSets
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DB("Data Source=.\\;Initial Catalog=DataSetsDB;Integrated Security=True"))
            {
                Console.WriteLine("SELECT *:");
                db.printEmailValues();
                Console.WriteLine();

                Console.WriteLine("After INSERT:");
                db.InsertIntoEmailTable();
                db.printEmailValues();
                Console.WriteLine();

                Console.WriteLine("After UPDATE:");
                db.UpdateEmailTable();
                db.printEmailValues();
                Console.WriteLine();

                Console.WriteLine("After DELETE:");
                db.DeleteFromEmailTable();
                db.printEmailValues();
                Console.WriteLine();
            }
        }
    }
}
