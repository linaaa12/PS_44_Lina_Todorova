using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    class UserContextSingleton
    {
        private static UserContext instance;

        private UserContextSingleton()
        {
            instance = new UserContext();
        }

        public static UserContext GetInstance()
        {
            if (instance == null)
            {
                instance = new UserContext();
            }
            return instance;
        }
    }
}
