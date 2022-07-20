using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityProject.Services.DTOs
{
    public class SubjectDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCourse { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
