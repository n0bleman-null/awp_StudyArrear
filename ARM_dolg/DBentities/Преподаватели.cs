using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class Преподаватели
    {
        public Преподаватели()
        {
            ГруппыПреподаватели = new HashSet<ГруппыПреподаватели>();
        }

        public long Id { get; set; }
        public string Фио { get; set; }
        public string Пароль { get; set; }
        public bool Администратор { get; set; }

        public virtual ICollection<ГруппыПреподаватели> ГруппыПреподаватели { get; set; }
    }
}
