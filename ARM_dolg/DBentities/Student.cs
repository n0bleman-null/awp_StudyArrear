using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class Student
    {
        public Student()
        {
            StudentLabs = new HashSet<StudentLab>();
        }

        public long Id { get; set; }
        public string Фио { get; set; }
        public string Пароль { get; set; }
        public long НомерГруппы { get; set; }

        public virtual StudGroup НомерГруппыNavigation { get; set; }
        public virtual ICollection<StudentLab> StudentLabs { get; set; }
        public override string ToString()
            => String.Join(" ", Фио, НомерГруппыNavigation.Номер);
    }
}
