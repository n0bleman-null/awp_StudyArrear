using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class Teacher
    {
        public Teacher()
        {
            GroupTeachers = new HashSet<GroupTeacher>();
        }

        public long Id { get; set; }
        public string Фио { get; set; }
        public string Пароль { get; set; }
        public bool Администратор { get; set; }

        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
        public override string ToString()
            => Фио;
    }
}
