using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
    public class StudentData
    {
        private static List<Student> testStudents = new List<Student>();
        public static void ResetTestStudentData()
        {
            testStudents.Add(new Student("Лина", "Иванова", "Тодоорва", "ФКСТ", "КСИ",
                "Бакалвър", new MainWindow().StudStatusChoices.ElementAt(0), 121220184, "123456", 4, 10, 44));
        }

        public static List<Student> TestStudents
        {
            get
            {
                ResetTestStudentData();
                return testStudents;
            }
            set { testStudents = value; }
        }

        private Student IsThereStudent(int facNum)
        {
            StudentInfoContext context = new StudentInfoContext();

            Student result =
            (from st in context.Students
             where st.FacultyNumber == facNum
             select st).First();
            return result;
        }

        public static double CalculateAverageGrade(Student student)
        {
            if (student.Grades != null && student.Grades.Any())
            {
                List<double> grades = student.Grades.Select(g =>(double)g.Value).ToList();
                double averageGrade = MathHelper.AverageCalculator(grades);
                return averageGrade;
            }

            return 0.0;
        }


    }
}
