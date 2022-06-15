using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace HelloCodeFirstLinq
{
    public class CodeFirstLingContext : DbContext
    {
        public CodeFirstLingContext()
            : base("data source=vm-dev-erp3;initial catalog=TestCodeFirstLing;integrated security=True;")
        {

        }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<CourseLecturer> CoursesLeturers { get; set; }
    }
}
