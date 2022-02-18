using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EntitiesDBEntities())
            {
                Console.WriteLine("SELECT *:");
                printAllEmailRows(context.email.ToList());
                Console.WriteLine();

                var email1 = new email()
                {
                    em_Id = 5,
                    em_value = "test@email.com",
                    lc_id = "L_6"
                };

                context.email.Add(email1);
                context.SaveChanges();

                Console.WriteLine("After INSERT:");
                printAllEmailRows(context.email.ToList());
                Console.WriteLine();

                email1.lc_id = "L_2";
                context.SaveChanges();

                Console.WriteLine("After UPDATE:");
                printAllEmailRows(context.email.ToList());
                Console.WriteLine();

                context.email.Remove(email1);
                context.SaveChanges();

                Console.WriteLine("After DELETE:");
                printAllEmailRows(context.email.ToList());
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private static void printAllEmailRows(IEnumerable<email> emails)
        {
            foreach (var email in emails)
                Console.WriteLine($"email id: {email.em_Id}, email: {email.em_value}, lecturer id: {email.lc_id}");
        }
    }
}
