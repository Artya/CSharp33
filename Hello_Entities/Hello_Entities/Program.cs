using System;
using System.Collections.Generic;

namespace Hello_Entities
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbWork = new DBWork();
            var dict = new Dictionary<string, string>();
            if (dbWork.LecturerCheck())
            {
                Console.WriteLine("Ok");
                dbWork.LecturerFind("L_2");
                dbWork.LecturerPhoneUpdate("L_1","78_98765");
                dbWork.LecturerEntityReader("City", "Hartford", dict);
                Console.WriteLine("Lecturers from Hartford");
                foreach (var item in dict)
                {
                    Console.WriteLine("Lecturer: {0} {1}", item.Key, item.Value);
                }

                Console.ReadKey();

                return;
            }
             
            Console.WriteLine("Failure"); 
        }
    }
}
