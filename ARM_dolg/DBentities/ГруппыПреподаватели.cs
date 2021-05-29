using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class ГруппыПреподаватели
    {
        public ГруппыПреподаватели()
        {
            ПрактическиеРаботы = new HashSet<ПрактическиеРаботы>();
        }

        public long Id { get; set; }
        public long Преподаватель { get; set; }
        public long НомерГруппы { get; set; }
        public long УчебныйПредмет { get; set; }

        public virtual Группы НомерГруппыNavigation { get; set; }
        public virtual Преподаватели ПреподавательNavigation { get; set; }
        public virtual УчебныеПредметы УчебныйПредметNavigation { get; set; }
        public virtual ICollection<ПрактическиеРаботы> ПрактическиеРаботы { get; set; }
    }
}
