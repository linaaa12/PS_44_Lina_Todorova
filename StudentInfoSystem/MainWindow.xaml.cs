using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

namespace StudentInfoSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> StudStatusChoices { get; set; }
        public string FacultyNumber { get; set; }
        public string Password { get; set; }
        private bool flag = false;

        public MainWindow()
        {
            SwitchLanguage("bg");

            InitializeComponent();
            FillStudStatusChoices();
        }

        private void MenuItem_BG(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("bg");
        }

        private void SwitchLanguage(string language)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch(language)
            {
                case "bg":
                    dictionary.Source = new Uri("..\\Resources\\Resources.bg-BG.xaml", UriKind.Relative);
                    break;
                case "en":
                    dictionary.Source = new Uri("..\\Resources\\Resources.en-US.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("..\\Resources\\Resources.bg-BG.xaml", UriKind.Relative);
                    break;

            }
            this.Resources.MergedDictionaries.Add(dictionary);
        }

        private void MenuItem_EN(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("en");
        }

        private void FillStudStatusChoices()
        {
            StudStatusChoices = new List<string>();
            using (IDbConnection connection = new
            SqlConnection(Properties.Settings.Default.DbConnect))
            {
                string sqlquery =
                @"SELECT StatusDescr FROM StudStatus";
                IDbCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                command.CommandText = sqlquery;
                IDataReader reader = command.ExecuteReader();
                bool notEndOfResult;
                notEndOfResult = reader.Read();
                while (notEndOfResult)

                {
                    string s = reader.GetString(0);
                    StudStatusChoices.Add(s);
                    notEndOfResult = reader.Read();
                }
            }
        }

        private static List<Student> GetStudentsFromDB()
        {
            StudentInfoContext context = StudentInfoContextSingleton.GetInstance();
            List<Student> students = context.Students.ToList();
            return students;
        }

        private void DeleteStudents(int fnum)
        {
            StudentInfoContext context = StudentInfoContextSingleton.GetInstance();
            Student delObj = context.Students.Where(x => x.FacultyNumber == fnum).FirstOrDefault();
            context.Students.Remove(delObj);
            context.SaveChanges();
        }

        private bool TestStudentsIfEmpty()
        {
            StudentInfoContext context = StudentInfoContextSingleton.GetInstance();
            int countStudents = context.Students.Count();
            if (context.Students.Count() == 0) return true;
            else return false;
        }
        private void CopyTestStudents()
        {
            StudentInfoContext context = StudentInfoContextSingleton.GetInstance();
            foreach (Student st in StudentData.TestStudents)
            {
                context.Students.Add(st);
            }
            context.SaveChanges();
        }


        private void logButton_Click(object sender, RoutedEventArgs e)
        {
            if (logButton.Content.ToString().Equals("Вход"))
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (TestStudentsIfEmpty())
                {
                    CopyTestStudents();
                }
                foreach (var item in PersonalInfoGrid.Children)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).IsEnabled = true;
                    }
                }
                foreach (var item in StudentInfoGrid.Children)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).IsEnabled = true;
                    }
                }
                Student student = LoginWindow.Student;
                firstNameText.Text = student.FirstName;
                secondNameText.Text = student.SecondName;
                lastNameText.Text = student.LastName;
                facultyText.Text = student.Faculty;
                majorText.Text = student.Major;
                degreeText.Text = student.Degree;
                statusText.Text = student.Status;
                facultyNumberText.Text = student.FacultyNumber.ToString();
                courseText.Text = student.Course.ToString();
                streamText.Text = student.Stream.ToString();
                groupText.Text = student.Group.ToString();
                logButton.Content = "Изход";
            }
            else if (logButton.Content.ToString().Equals("Изход"))
            {
                foreach (var item in PersonalInfoGrid.Children)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Text = "";
                        ((TextBox)item).IsEnabled = false;
                    }
                }
                foreach (var item in StudentInfoGrid.Children)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Text = "";
                        ((TextBox)item).IsEnabled = false;
                    }
                }
                logButton.Content = "Вход";
            }
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            StudentInfoContext context = StudentInfoContextSingleton.GetInstance();

            if (gradeText.Text != "")
            {
                int gradeValue = int.Parse(gradeText.Text);
                Grade grade = new Grade();
                grade.Value = gradeValue;
                grade.Student = context.Students.Where(s => s.FirstName == firstNameText.Text).FirstOrDefault();
                context.Grades.Add(grade);
                context.SaveChanges();
                gradeText.Text = "";

                Student student = context.Students.Where(s => s.FirstName == firstNameText.Text).FirstOrDefault();
                double averageGrade = StudentData.CalculateAverageGrade(student);
                averageGradeText.Text = averageGrade.ToString();
            }
        }
    }
}
