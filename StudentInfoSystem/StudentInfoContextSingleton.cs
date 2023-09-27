using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
    public class StudentInfoContextSingleton
    {
        private static StudentInfoContext instance;

        private StudentInfoContextSingleton()
        {
            if (instance == null)
            {
                instance = new StudentInfoContext();
            }
        }

        public static StudentInfoContext GetInstance()
        {
            if (instance == null)
            {
                instance = new StudentInfoContext();
            }
            return instance;
        }
    }
}
