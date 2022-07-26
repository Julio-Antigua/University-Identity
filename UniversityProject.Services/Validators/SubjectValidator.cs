using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Validators
{
    public class SubjectValidator : AbstractValidator<SubjectDto>
    {
        public SubjectValidator()
        {
            RuleFor(subjetc => subjetc.Name)
                .NotEmpty().When(f => f.Name == null).WithMessage("The name field can't be empty");

            RuleFor(subject => subject.IdCourse)
                .NotEmpty().When(f => f.IdCourse <= 0).WithMessage("The idcourse field can't be empty");

            RuleFor(subject => subject.StartTime)
                .Must(d => Convert.ToDateTime(d) >= DateTime.Now).WithMessage("The starttime can't be menol to the current date");

            RuleFor(subject => subject.EndTime)
                .Must(d => Convert.ToDateTime(d) >= DateTime.Now).WithMessage("The endtime can't be menol to the current date");


        }
    }
}
