using System;
using System.Collections.Generic;

#nullable disable

namespace ARM_dolg
{
    public partial class Студенты
    {
        public Студенты()
        {
            СтудентыПрактическиеРаботы = new HashSet<СтудентыПрактическиеРаботы>();
        }

        public long Id { get; set; }
        public string Фио { get; set; }
        public string Пароль { get; set; }
        public long НомерГруппы { get; set; }

        public virtual Группы НомерГруппыNavigation { get; set; }
        public virtual ICollection<СтудентыПрактическиеРаботы> СтудентыПрактическиеРаботы { get; set; }
    }
}
