using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace ARM_dolg
{
    public partial class GroupTeacher
    {
        public GroupTeacher()
        {
            Labs = new HashSet<Lab>();
        }

        public long Id { get; set; }
        public long Преподаватель { get; set; }
        public long НомерГруппы { get; set; }
        public long УчебныйПредмет { get; set; }

        public virtual StudGroup НомерГруппыNavigation { get; set; }
        public virtual Teacher ПреподавательNavigation { get; set; }
        public virtual StudSubject УчебныйПредметNavigation { get; set; }
        public virtual ICollection<Lab> Labs { get; set; }

        public override string ToString()
            => string.Join(" ", ПреподавательNavigation.ToString(), НомерГруппыNavigation.ToString(), УчебныйПредметNavigation.ToString());
    }
}
