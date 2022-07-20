using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;

namespace UniversityProject.Infrastructure.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        //Task<IEnumerable<Student>> GetAllBySubject(DetailsSubject details);

    }
}
