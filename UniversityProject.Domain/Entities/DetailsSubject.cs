#nullable disable

namespace UniversityProject.Domain.Entities
{
    public partial class DetailsSubject
    {
        public int IdSubject { get; set; }
        public int IdStudent { get; set; }

        public virtual Student IdStudentNavigation { get; set; }
        public virtual Subject IdSubjectNavigation { get; set; }
    }
}
