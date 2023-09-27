using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace UserLogin
{
    class Program
    {
        public List<string> StudStatusChoices { get; set; }

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

        private void AddUsers()
        {
            UserContext context = new UserContext();
            UserLogin.UserData.ResetTestUserData();
            context.Users.AddRange(UserLogin.UserData.testUsers);
            context.SaveChanges();
            //UserLogin.UserData.testUsers
        }

        static void Main(string[] args)
        {
            string username;
            string password;

            Console.WriteLine("username: ");
            username = Console.ReadLine();
            Console.WriteLine("password: ");
            password = Console.ReadLine();
            Console.WriteLine("\n");

            User user = null;
            LoginValidation loginValidation = new LoginValidation(username, password, actionOnError);

            if (loginValidation.ValidateUserInput(ref user))
            {
                Console.WriteLine(user.toString());
                switch (LoginValidation.currentUserRole)
                {
                    case UserRoles.ANONYMOUS:
                        Console.WriteLine("User not found!");
                        break;
                    case UserRoles.ADMIN:
                        Console.WriteLine("System adminstrator has logged.");
                        break;
                    case UserRoles.INSPECTOR:
                        Console.WriteLine("Inspector has logged.");
                        break;
                    case UserRoles.PROFESSOR:
                        Console.WriteLine("Professor has logged.");
                        break;
                    case UserRoles.STUDENT:
                        Console.WriteLine("Student has logged.");
                        break;
                }
                AdministratorActivity();
            }
        }

        static void actionOnError(string error)
        {
            Console.WriteLine("!!! " + error + " !!!");
        }

        static void AdministratorActivity()
        {
            for (; ; )
            {
                Console.WriteLine("\n");
                Console.WriteLine("Choose option:");
                Console.WriteLine("0: Exit");
                Console.WriteLine("1: Change user role");
                Console.WriteLine("2: Change user active until");
                Console.WriteLine("3: List of active users: ");
                Console.WriteLine("4: View log activity: ");
                Console.WriteLine("5: View current activity: ");

                String option = Console.ReadLine();
                if (option.Equals("0"))
                {
                    break;
                }

                string userName = "";
                if (option.Equals("1") || option.Equals("2"))
                {
                    Console.WriteLine("Enter username: ");
                    userName = Console.ReadLine();

                }

                IEnumerable<string> currentActs;
                StringBuilder sb = new StringBuilder();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Enter new role:");
                        int newRole = Convert.ToInt32(Console.ReadLine());
                        UserData.AssignUserRole(1, (UserRoles)newRole);
                        break;
                    case "2":
                        Console.WriteLine("Enter new activeUntil date:");
                        DateTime newDate = DateTime.Parse(Console.ReadLine());
                        UserData.SetUserActiveUntil(userName, newDate);
                        break;
                    case "3":
                        UserData.testUsers.ForEach(u => Console.WriteLine(u.username));
                        break;
                    case "4":
                        currentActs =
                        Logger.OuputlogFile();
                        Console.WriteLine(string.Join("", currentActs));
                        break;
                    case "5":
                        Console.WriteLine("Enter filter: ");
                        string filter = Console.ReadLine();
                        currentActs =
                        Logger.GetCurrentSessionActivities(filter);
                        Console.WriteLine(string.Join("", currentActs));
                        break;
                    default:
                        Console.WriteLine("Wrong opiton!");
                        break;
                }
            }
        }
    }
}
