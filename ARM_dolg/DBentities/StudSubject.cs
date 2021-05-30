using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class StudSubject
    {
        public StudSubject()
        {
            GroupTeachers = new HashSet<GroupTeacher>();
        }

        public long Id { get; set; }
        public string Название { get; set; }

        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
        public override string ToString()
            => Название;
    }
}
