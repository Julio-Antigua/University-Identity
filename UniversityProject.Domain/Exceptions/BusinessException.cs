using System;

namespace UniversityProject.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {

        }
        public BusinessException(string message): base(message)
        {

        }
    }
}
