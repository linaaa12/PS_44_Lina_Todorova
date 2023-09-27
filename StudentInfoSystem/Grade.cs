using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoSystem
{
    public class Grade
    {
        public int GradeId { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int Value { get; set; }

        public Grade()
        {

        }
    }
}
