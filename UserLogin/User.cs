using System;

namespace UserLogin
{
    public class User
    {
        public System.Int32 UserId { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int facultyNumber { get; set; }
            public int role { get; set; }
            public DateTime created { get; set; }
            public DateTime activeUntil { get; set; }

        public string toString()
        {
            return $"{this.username} has a password: {this.password}. "
                + $"The person has faculty number: {this.facultyNumber}. "
                + $"His role is {this.role}. "
                + $"The user is created on {this.created.Day}.{this.created.Month}.{this.created.Year}. "
                + $"The user is active until {this.activeUntil.Day}.{this.activeUntil.Month}.{this.activeUntil.Year}. ";
        }
    }
}
