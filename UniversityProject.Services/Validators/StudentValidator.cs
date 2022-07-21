using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Validators
{
    public class StudentValidator : AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            RuleFor(student => student.FirstName)
                .NotEmpty().When(f => f.FirstName == null).WithMessage("The firstname field cannot be empty.")
                .MaximumLength(25).WithMessage("The firstname field has exceeded the maximum number(25) of characters");

            RuleFor(student => student.LastName)
                .NotEmpty().When(l => l.LastName == null).WithMessage("The lastname field cannot be empty.")
                .MaximumLength(25).WithMessage("The lastname field has exceeded the maximum number(25) of characters");

            RuleFor(student => student.DateOfBirth)
                .Must(d => Convert.ToDateTime(d) <= DateTime.Now).WithMessage("The date cannot exceed the current date");

            RuleFor(student => student.Email)
                .NotEmpty().When(e => e.Email == null).WithMessage("The email field cannot be empty")
                .EmailAddress().WithMessage("The mail is invalid");
        }
    }
}
