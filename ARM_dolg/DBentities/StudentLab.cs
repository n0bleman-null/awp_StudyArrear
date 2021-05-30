using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class StudentLab
    {
        public long Id { get; set; }
        public long ПрактическияРабота { get; set; }
        public long Студент { get; set; }
        public string Статус { get; set; }

        public virtual Lab ПрактическияРаботаNavigation { get; set; }
        public virtual Student СтудентNavigation { get; set; }
        public override string ToString()
            => string.Join(" ", ПрактическияРаботаNavigation.ToString(), СтудентNavigation.ToString(), Статус);
    }
}
