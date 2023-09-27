using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
    public class Student
    {
        public Student(string firstName, string secondName, string lastName, string faculty,
   string major, string degree, string status, int facultyNumber, string password, int course, int stream, int group)
        {
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            Faculty = faculty;
            Major = major;
            Degree = degree;
            Status = status;
            FacultyNumber = facultyNumber;
            Password = password;
            Course = course;
            Stream = stream;
            Group = group;
        }
        public Student()
        {

        }

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public string Status { get; set; }
        public int FacultyNumber { get; set; }
        public string Password { get; set; }
        public int Course { get; set; }
        public int Stream { get; set; }
        public int Group { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
