using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UniversityProject.Domain.Enumerations
{
    public class Helpers
    {
        public static string xmlFile => $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        public static string Uri => "/api/Student";
    }
}
