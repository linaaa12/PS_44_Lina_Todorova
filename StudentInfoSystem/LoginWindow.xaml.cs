using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentInfoSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public int FacultyNumber { get; set; }
        public SecureString Password { get; set; }
        public static Student Student { get; set; } = null;

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.SecurePassword;
        }

        private void submitButtom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            StudentInfoContext context = StudentInfoContextSingleton.GetInstance();
            int facultyNumber = int.Parse(usernameText.Text);
            string password = new System.Net.NetworkCredential(string.Empty, passwordBox.SecurePassword).Password;

            if (context.Students.Where(s => s.FacultyNumber == facultyNumber && s.Password.Equals(password))
                .FirstOrDefault() != null)
            {
                Student = context.Students.Where(s => s.FacultyNumber == facultyNumber && s.Password.Equals(password))
                .FirstOrDefault();
                this.Close();
            }
            else
            {
                MessageBox.Show("Невалидни данни! Моля опитайте отново");
            }

        }
    }
}
