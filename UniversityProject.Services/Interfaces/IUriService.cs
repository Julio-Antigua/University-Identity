using System;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Services.Interfaces
{
    public interface IUriService 
    {
        Uri GetStudentPaginationUri(StudentQueryFilter filter, int numPag, string route);
    }
}
