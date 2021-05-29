using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class ПрактическиеРаботы
    {
        public ПрактическиеРаботы()
        {
            СтудентыПрактическиеРаботы = new HashSet<СтудентыПрактическиеРаботы>();
        }

        public long Id { get; set; }
        public long ГруппаПреподаватель { get; set; }
        public DateTime ДатаЗанятия { get; set; }

        public virtual ГруппыПреподаватели ГруппаПреподавательNavigation { get; set; }
        public virtual ICollection<СтудентыПрактическиеРаботы> СтудентыПрактическиеРаботы { get; set; }
    }
}
