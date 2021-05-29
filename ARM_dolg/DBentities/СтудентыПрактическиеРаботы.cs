using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class СтудентыПрактическиеРаботы
    {
        public long Id { get; set; }
        public long ПрактическияРабота { get; set; }
        public long Студент { get; set; }
        public string Статус { get; set; }

        public virtual ПрактическиеРаботы ПрактическияРаботаNavigation { get; set; }
        public virtual Студенты СтудентNavigation { get; set; }
    }
}
