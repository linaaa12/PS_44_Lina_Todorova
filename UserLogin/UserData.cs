using System;
using System.Collections.Generic;
using System.Linq;

namespace UserLogin
{
    public static class UserData
    {
        private static List<User> _testUsers = new List<User>(3);
        public static List<User> testUsers
        {
            get
            {
                return _testUsers;
            }
            set { _testUsers = value; }
        }

        public static void ResetTestUserData()
        {
            User testUser = new User();

            testUser.username = "Lina";
            testUser.password = "123";
            testUser.facultyNumber = 121220184;
            testUser.role = 1;
            testUser.created = DateTime.Now;
            testUser.activeUntil = DateTime.MaxValue;
            _testUsers.Add(testUser);

            testUser = new User();
            testUser.username = "Mila";
            testUser.password = "345";
            testUser.facultyNumber = 121220049;
            testUser.role = 4;
            testUser.created = DateTime.Now;
            testUser.activeUntil = DateTime.MaxValue;
            _testUsers.Add(testUser);

            testUser = new User();
            testUser.username = "Kalina";
            testUser.password = "567";
            testUser.facultyNumber = 121220126;
            testUser.role = 4;
            testUser.created = DateTime.Now;
            testUser.activeUntil = DateTime.MaxValue;
            _testUsers.Add(testUser);
        }

        public static User IsUserPassCorrect(String username, String password)
        {
           bool isUserExcsist = _testUsers.Any(user => user.username.Equals(username) && user.password.Equals(password));

            if (isUserExcsist)
            {
                return (from user in _testUsers
                        where user.username.Equals(username)
                        && user.password.Equals(password)
                        select user).First();
            } else
            {
                throw new InvalidOperationException("Невалиден потребител или парола");
            }
        }

        public static bool SetUserActiveUntil(String username, DateTime newActiveUntil)
        {
            foreach (User user in _testUsers)
            {
                if (user.username == username)
                {
                    user.activeUntil = newActiveUntil;
                    Logger.LogActivity(Activities.USER_ACTIVE_UNTIL_CHANGED);
                    return true;
                }
            }
            return false;
        }

        public static bool AssignUserRole(int userid, UserRoles newRole)
        {
            UserContext context = new UserContext();

            User usr =
            (from u in UserData.testUsers
             where u.UserId == userid
             select u).First();
            usr.role = (int)newRole;
            context.SaveChanges();
            Logger.LogActivity(Activities.USER_ROLE_CHANGED);

            if (usr != null)
            {
                return true;
            }
            return false;
        }
    }
}
