using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class УчебныеПредметы
    {
        public УчебныеПредметы()
        {
            ГруппыПреподаватели = new HashSet<ГруппыПреподаватели>();
        }

        public long Id { get; set; }
        public string Название { get; set; }

        public virtual ICollection<ГруппыПреподаватели> ГруппыПреподаватели { get; set; }
    }
}
