using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.EntityClient;
using System.Data;
using System.Configuration;

namespace Hello_Entities
{
    class DBWork
    {
        public bool LecturerCheck()
        {
            try
            {
                using (var context = new EntitiesCourses())
                {
                    var lecture = context.Lecturers.FirstOrDefault<Lecturers>();
                    Console.WriteLine($"Last lecturer ID {lecture.LecturerId}, name {lecture.LectureFirstName} {lecture.LectureLastName}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool LecturerFind(string key)
        {
            try
            {
                using (var context = new EntitiesCourses())
                {
                    var lecture = context.Lecturers.Find(key);

                    if (lecture == null)
                        return false;

                    Console.WriteLine($"Find lecturer: ID {lecture.LecturerId}, name {lecture.LectureFirstName} {lecture.LectureLastName}");
                    
                    return true;
                }                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool LecturerPhoneUpdate(string key, string phone)
        {
            try
            {
                using (var context = new EntitiesCourses())
                {
                    var lecture = context.Lecturers.Find(key);

                    if (lecture == null)
                        return false;

                    Console.WriteLine($"Find lecturer: ID {lecture.LecturerId}, name {lecture.LectureFirstName} {lecture.LectureLastName} Phone {lecture.Phone}");
                    lecture.Phone = phone;
                    Console.WriteLine($"Phone updated to {phone}");
                    context.SaveChanges();
                    
                    return true;
                }                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool LecturerEntityReader(string column, string columnValue, Dictionary<string, string> dict)
        {
            try
            {
                using (var entityConnection = new EntityConnection("name=EntitiesCourses"))
                {
                    entityConnection.Open();
                    var command = entityConnection.CreateCommand();
                    command.CommandText = $@"SELECT lec.LectureFirstName, lec.LectureLastName FROM EntitiesCourses.Lecturers AS lec where lec.{column}='{columnValue}'";

                    using (var reader = command.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        { 
                            var name = reader.GetString(0);
                            var lastname = reader.GetString(1);

                            dict.Add(name, lastname);
                        }

                        return true;
                    }                        
                }                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
