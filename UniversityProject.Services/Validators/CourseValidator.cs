using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Validators
{
    public class CourseValidator : AbstractValidator<CourseDto>
    {
        public CourseValidator()
        {
            RuleFor(course => course.Name)
                .NotEmpty().When(d => d.Name == null).WithMessage("The name field can't be empty");
        }
    }
}
