using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityProject.Services.QueryFilters
{
    public class StudentQueryFilter
    {
        public int? IdStudent { get; set; }
        public string FirstName { get; set; }
        public DateTime? EntryDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
