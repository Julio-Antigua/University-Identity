using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Services.Interfaces
{
    public interface IUriService 
    {
        Uri GetStudentPaginationUri(StudentQueryFilter filter, string actionUrl);
    }
}
