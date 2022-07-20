using System;
using System.Collections.Generic;

#nullable disable

namespace UniversityProject.Domain.Entities
{
    public partial class Subject : BaseEntity
    {
        public Subject()
        {
            DetailsSubjects = new HashSet<DetailsSubject>();
        }

        public string Name { get; set; }
        public int IdCourse { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual Course IdCourseNavigation { get; set; }
        public virtual ICollection<DetailsSubject> DetailsSubjects { get; set; }
    }
}
