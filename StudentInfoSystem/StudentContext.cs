using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
        public class StudentInfoContext : DbContext
        {
            public StudentInfoContext() : base(Properties.Settings.Default.DbConnect)
            { }
            public DbSet<Student> Students { get; set; }
            public DbSet<Grade> Grades { get; set; }

            public List<Student> GetStudentInfo(int facultyNumber)
            {
            var facultyNumberParameter = new SqlParameter("@FacultyNumber", facultyNumber);
            return Database.SqlQuery<Student>("GetStudentInfo @FacultyNumber", facultyNumberParameter).ToList();

            }
        }
}
