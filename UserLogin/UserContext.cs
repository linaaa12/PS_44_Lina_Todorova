using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace UserLogin
{
    public class UserContext : DbContext
    {
        public UserContext() : base(Properties.Settings.Default.DbConnect)
        { }
        public DbSet<User> Users { get; set; }
    }
}
