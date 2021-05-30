using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class Lab
    {
        public Lab()
        {
            StudentLabs = new HashSet<StudentLab>();
        }

        public long Id { get; set; }
        public long ГруппаПреподаватель { get; set; }
        public DateTime ДатаЗанятия { get; set; }

        public virtual GroupTeacher ГруппаПреподавательNavigation { get; set; }
        public virtual ICollection<StudentLab> StudentLabs { get; set; }
        public override string ToString()
            => string.Join(" ", ГруппаПреподавательNavigation.ToString(), ДатаЗанятия.ToShortDateString());
    }
}
