using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class Группы
    {
        public Группы()
        {
            ГруппыПреподаватели = new HashSet<ГруппыПреподаватели>();
            Студенты = new HashSet<Студенты>();
        }

        public long Id { get; set; }
        public string Номер { get; set; }

        public virtual ICollection<ГруппыПреподаватели> ГруппыПреподаватели { get; set; }
        public virtual ICollection<Студенты> Студенты { get; set; }
    }
}
