using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
    public static class MathHelper
    {
        public static double AverageCalculator(List<double> grades)
        { 
                double averageGrade = grades.Average();
                return Math.Round(averageGrade, 2);

        }

    }
}