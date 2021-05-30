using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class StudGroup
    {
        public StudGroup()
        {
            GroupTeachers = new HashSet<GroupTeacher>();
            Students = new HashSet<Student>();
        }

        public long Id { get; set; }
        public string Номер { get; set; }

        public virtual ICollection<GroupTeacher> GroupTeachers { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public override string ToString()
            => Номер;
    }
}
