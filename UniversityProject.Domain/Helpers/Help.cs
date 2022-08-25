using System.Reflection;

namespace UniversityProject.Domain.Helpers
{
    public class Help
    {
        public static string xmlFile => $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        public static string Uri => "/api/Student";
    }
}
