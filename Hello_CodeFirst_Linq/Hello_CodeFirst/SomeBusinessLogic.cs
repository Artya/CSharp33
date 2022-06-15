using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.IO;

namespace HelloCodeFirstLinq
{
    public class SomeBusinessLogic : IDisposable
    {
        private CodeFirstLingContext context;
        private bool disposed = false;

        public SomeBusinessLogic()
        {
            this.context = new CodeFirstLingContext();
        }

        public void Initialize()
        {
            try
            {
                context.Database.Initialize(false);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Initialization error ?\r\n", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                this.disposed = true;
            }
        }

        public bool ClearAndInsertByScript()
        {
            try
            {
                using (var reader = new StreamReader("insrt_ling.sql"))
                {
                    while (true)
                    {
                        var currentQuery = reader.ReadLine();

                        if (currentQuery == null)
                            return true;

                        this.context.Database.ExecuteSqlCommand(currentQuery);
                        Console.WriteLine($"Executed query: {currentQuery}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void ShowLecturers()
        {
            var lecturers = context.Lecturers.ToList<Lecturer>();

            foreach (var lecturer in lecturers)
            {
                Console.WriteLine(lecturer);
            }
        }

        public void InsertLecturerWithEMail()
        {
            using (var dbContextTransaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("Enter lecturer ID");
                    var lecturerID = Console.ReadLine();
                    var newLecturer = new Lecturer() { ID = lecturerID, FirstName = $"{lecturerID} first name" };
                    this.context.Lecturers.Add(newLecturer);
                    this.context.SaveChanges();

                    var newEmail = new Email() { EmailValue = $"{lecturerID}newEmail@{lecturerID}email.ua", Lecturer = newLecturer, LecturerID = newLecturer.ID };
                    this.context.Emails.Add(newEmail);
                    this.context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void DeleteLecturerWithEMail()
        {
            using (var dbContextTransaction = this.context.Database.BeginTransaction())
            {
                try
                {
                    var searchedLecturer = AskLekturer();

                    if (searchedLecturer == null)
                        return;

                    var emails = this.context.Emails.Where<Email>(email => email.LecturerID == searchedLecturer.ID);

                    if (emails != null)
                    {
                        foreach (var email in emails)
                        {
                            this.context.Emails.Remove(email);
                        }

                        this.context.SaveChanges();
                    }

                    this.context.Lecturers.Remove(searchedLecturer);
                    this.context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void AddEmailToLecturer()
        {
            var searchedLecturer = AskLekturer();

            if (searchedLecturer == null)
                return;

            Console.WriteLine("Enter E-Mail value:");
            var emailValue = Console.ReadLine();

            var newEmail = new Email() { EmailValue = emailValue, Lecturer = searchedLecturer, LecturerID = searchedLecturer.ID};
            this.context.Emails.Add(newEmail);
            this.context.SaveChanges();
        }

        public void EditLecturer()
        {
            var searchedLecturer = AskLekturer();

            if (searchedLecturer == null)
                return;

            Console.WriteLine("Enter new name");
            var newName = Console.ReadLine();

            searchedLecturer.FirstName = newName;

            this.context.SaveChanges();
        }

        public Lecturer AskLekturer()
        {
            Console.WriteLine("Enter lecturer ID");
            var lecturerID = Console.ReadLine();

            var searchedLecturer = this.context.Lecturers.FirstOrDefault<Lecturer>(lecturer => lecturer.ID == lecturerID);

            if (searchedLecturer == null)
            {
                Console.WriteLine($"Lecturer with ID {lecturerID} not found");
                return null;
            }

            return searchedLecturer;
        }
    }
}


