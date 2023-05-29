using Microsoft.EntityFrameworkCore;
using static MysqlEFCore6.Program;

namespace MysqlEFCore6
{
    class Program
    {
        //Demonstrates how to get started using Oracle Entity Framework Core 6 
        //Code connects to on-premises Oracle DB or walletless Oracle Autonomous DB

        public class SchoolContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EFCore-SchoolDB;Trusted_Connection=True");
                //optionsBuilder.UseOracle(@"User Id=blog;Password=<Password>;Data Source=<Net Service Name>");
                optionsBuilder.UseMySQL("server=localhost;user=root;password=12345678;database=semana10");
            }

            public DbSet<Student>? Students { get; set; }

        }

        public class Student
        {
            public Student()
            {
            }

            public int StudentID { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }

        }


        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                context.Database.EnsureCreated();
                Console.WriteLine("Database, verificada!");
            }

            //Insert Data
            using (var context = new SchoolContext())
            {
                var std = new Student()
                {
                    FirstName = "John",
                    LastName = "Doe"
                };
                context.Students.Add(std);
                Console.WriteLine("Database, .Add(std)!");
                // or
                var std2 = new Student()
                {
                    FirstName = "John 2",
                    LastName = "Doe 2"
                };
                Console.WriteLine("Database, Add<Student>(std2)!");
                context.Add<Student>(std2);
                context.SaveChanges();
            }
            
            //Updating Data
            using (var context = new SchoolContext())
            {
                var std = context.Students.First<Student>();
                std.FirstName = "Jason";
                context.SaveChanges();
            }

            //Deleting Data
            using (var context = new SchoolContext())
            {
                var std = context.Students.First<Student>();
                context.Students.Remove(std);
                // or
                // context.Remove<Student>(std);
                context.SaveChanges();
            }

        }
    }
}
