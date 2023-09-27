using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
    public class StudentValidation
    {
        public Student GetStudentDataByUser(UserLogin.User user)
        {
            Student student = new Student()
            {
                FacultyNumber = user.facultyNumber
            };
            return student;
        }
    }
}
