using System;

namespace Hello_DataSets
{
    class Program
    {

        static void Main(string[] args)
        {
            var myDbWork = new DBWork("Data Source=vm-dev-erp3;Initial Catalog=E114788673E434646D04E6B4E0A31008_NBASKET\\" +
            "GROUP_09032016\\270416_22\\LAB_1_7_4\\UPGRADE\\BEGIN\\ADO_NET_174\\BEGIN\\TECT_DATASETS" +
            "_DB.MDF;Integrated Security=True");
            
            myDbWork.DBConnect();

            Console.WriteLine("Reading courses table");
            myDbWork.CoursesRead("courses");
            Console.WriteLine();

            Console.WriteLine("Update courses table");
            myDbWork.CoursesUpdate("courses", "course_id", "C13", "begin_date", "2014-01-06");

            Console.WriteLine("Reading courses table");
            myDbWork.CoursesRead("courses");
            Console.WriteLine();

            Console.WriteLine("Update courses table by DataSet");
            myDbWork.CoursesUpdateDataSet("courses", "course_id", "C13", "begin_date", "2013-01-01");

            Console.WriteLine("Reading courses table");
            myDbWork.CoursesRead("courses");
            Console.WriteLine();

            Console.WriteLine("Update courses table by Builder");
            myDbWork.CoursesUpdateBuilder("courses", "course_id", "C13", "begin_date", "2012-01-01");

            Console.WriteLine("Reading courses table");
            myDbWork.CoursesRead("courses");
            Console.WriteLine();

            Console.WriteLine("Insert  to courses table by Builder");
            string[] clmns = new string[] { "course_name", "contract", "facl_id" };
            string[] clmn_values = new string[] { "biology", "0", "" };
            myDbWork.CoursesInsertBuilder("courses", "course_id", clmns, clmn_values);

            Console.WriteLine("Reading courses table");
            myDbWork.CoursesRead("courses");
            Console.WriteLine();

            Console.WriteLine("Delete from courses table");

            Console.WriteLine("Write  key to delete: \r\n");
            string delKey = Console.ReadLine();
            myDbWork.CoursesDelete("courses", "course_id", delKey);

            Console.WriteLine("Reading courses table");
            myDbWork.CoursesRead("courses");

            Console.ReadKey();
        }
    }
}
