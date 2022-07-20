using System;
using System.Collections.Generic;

#nullable disable

namespace UniversityProject.Domain.Entities
{
    public partial class Course : BaseEntity
    {
        public Course()
        {
            Subjects = new HashSet<Subject>();
        }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
