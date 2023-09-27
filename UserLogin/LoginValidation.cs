using System;

namespace UserLogin
{
    public class LoginValidation
    {
        private static UserRoles _currentUserRole;
        public static UserRoles currentUserRole
        {
            get { return _currentUserRole; }
            private set { _currentUserRole = value; }
        }
        public static String username { get; set; }
        public static String password { get; set; }
        public static String error { get; set; }

        public delegate void ActionOnError(string error);
        private ActionOnError actionOnError;

        public LoginValidation(String _username, String _password, ActionOnError _actionOnError)
        {
            username = _username;
            password = _password;
            actionOnError = _actionOnError;
        }

        public bool ValidateUserInput(ref User user)
        {
            UserData.ResetTestUserData();

            Boolean succses = true;

            Boolean emptyUserName;
            emptyUserName = username.Equals(String.Empty);
            if (emptyUserName == true)
            {
                error = "Username is missing.";
                succses = false;
            }
            else if (username.Length < 5)
            {
                error = "Username's lenght must be at least 5 characters.";
                succses = false;
            }

            Boolean emptyPassword;
            emptyPassword = password.Equals(String.Empty);
            if (emptyPassword == true)
            {
                error = "Password is missing.";
                succses = false;
            }
            else if (password.Length < 5)
            {
                error = "Password's lenght must be at least 5 characters.";
                succses = false;
            }

            user = UserData.IsUserPassCorrect(username, password);
 
            if (user == null)
            {
                error = "Incorect user.";
                succses = false;
            }

            if (succses == false)
            {
                actionOnError(error);
                _currentUserRole = UserRoles.ANONYMOUS;
            }
            else
            {
                // Logger.LogActivity(Activities.USER_LOGED);
                // да попълва currentRole с ролята на намерения потребител само, ако има такъв.
                _currentUserRole = (UserRoles)user.role;
            }

            return succses;
        }
    }
}
