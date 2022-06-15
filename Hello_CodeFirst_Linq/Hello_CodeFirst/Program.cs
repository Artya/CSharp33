using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.EntityClient;
using System.Data;

namespace HelloCodeFirstLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var businessLogic = new SomeBusinessLogic())
            {  
                try
                {
                    businessLogic.Initialize();

                    int a;
                    
                    do
                    {
                        Console.WriteLine(@"Please,  type the number:                       
                        1.  Clear and insert to the DB by script
                        2.  Insert lecturer with email (one-to-many)
                        3.  Delete lecturer with email (one-to-many)
                        4.  Add new email to lecturer
                        5.  Update lecturer
                       
                        ");
                        try
                        {
                            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                            a = int.Parse(Console.ReadLine());
                            switch (a)
                            {
                                case 1:
                                    Console.WriteLine("Clear and insert to the DB by script  ");
                                    businessLogic.ClearAndInsertByScript();
                                    break;
                                case 2:
                                    Console.WriteLine("Insert lecturer with email (one-to-many) ");
                                    businessLogic.InsertLecturerWithEMail();
                                    businessLogic.ShowLecturers();
                                    break;
                                case 3:
                                    Console.WriteLine("Delete lecturer with email (one-to-many) ");
                                    businessLogic.DeleteLecturerWithEMail();
                                    businessLogic.ShowLecturers();
                                    break;
                                case 4:
                                    Console.WriteLine("Add new email to lecturer ");
                                    businessLogic.AddEmailToLecturer();
                                    businessLogic.ShowLecturers();
                                    break;
                                case 5:
                                    Console.WriteLine("Update lecturer name ");
                                    businessLogic.EditLecturer();
                                    businessLogic.ShowLecturers();
                                    break;
                                default:
                                    Console.WriteLine("Exit");
                                    break;
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                        finally
                        {
                            Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Press Spacebar to exit; press any key to continue");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    while (Console.ReadKey().Key != ConsoleKey.Spacebar);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();               
            }
        }             
    }
}

