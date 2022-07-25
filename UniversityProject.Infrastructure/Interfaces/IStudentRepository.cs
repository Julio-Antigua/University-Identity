using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;

namespace UniversityProject.Infrastructure.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        IEnumerable<DetailsStudent> GetAllBySubject(DetailsSubject details);
        IEnumerable<DetailsStudent> GetAllByStudent(DetailsSubject details);
    }
}
