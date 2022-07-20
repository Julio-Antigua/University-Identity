using System;
using System.Collections.Generic;

#nullable disable

namespace UniversityProject.Domain.Entities
{
    public partial class Student : BaseEntity
    {
        public Student()
        {
            DetailsSubjects = new HashSet<DetailsSubject>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<DetailsSubject> DetailsSubjects { get; set; }
    }
}
