using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Infrastructure.Options;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Services.Services
{
    public class StudentService : IStudentService   
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UniversityContext _context;
        private readonly PaginationOptions _paginationOptions;


        public StudentService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,IMapper mapper,UniversityContext context)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._context = context;
            this._paginationOptions = options.Value;

        }
        public PagedList<Student> GetAll(StudentQueryFilter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaulPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;

            IEnumerable<Student> students = _unitOfWork.StudentRepository.GetAll();

            if (filter.IdStudent != null)
            {
                students = students.Where(x => x.Id == filter.IdStudent);
            }
            if (filter.FirstName != null)
            {
                students = students.Where(x => x.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }
            if (filter.EntryDate != null)
            {
                students = students.Where(x => x.CreationDate.ToShortDateString() == filter.EntryDate?.ToShortDateString());
            }          

            PagedList<Student> pageStudent = PagedList<Student>.Create(students, filter.PageNumber, filter.PageSize);

            return pageStudent;
        }

        public async Task<StudentDto> GetById(int id)
        {
            Student student = await _unitOfWork.StudentRepository.GetById(id);
            if(student == null)
            {
                throw new BusinessException("this student does not exist");
            }
            StudentDto studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }

        public async Task Add(StudentDto studentsDto)
        {
            if (studentsDto.SubjectIds == null)
            {
                throw new BusinessException("you can`t create a student without subjects");
            }
            //IEnumerable<int> subjectIds = _unitOfWork.StudentRepository.GetAll().Where(x => studentsDto.SubjectIds.Contains(x.Id)).Select(x => x.Id).ToList();
            IEnumerable<int> subjectIds = _context.Subjects.Where(x => studentsDto.SubjectIds.Contains(x.Id)).Select(x => x.Id).ToList();
            if (studentsDto.SubjectIds.Count() != subjectIds.Count())
            {
                throw new BusinessException("there is not one of the submitted subject");
            }
            Student student = _mapper.Map<Student>(studentsDto);
            await _unitOfWork.StudentRepository.Add(student);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> UpdateById(int id,StudentDto studentDto)
        {
            Student student = _mapper.Map<Student>(studentDto);
            if (student.Id != id)
            {
                throw new BusinessException("this student does not exist");
            }
            student.Id = id;
            _unitOfWork.StudentRepository.UpdateById(student);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            Student student = await _unitOfWork.StudentRepository.GetById(id);
            if (student == null)
            {
                throw new BusinessException("this student does not exist");
            }
            await _unitOfWork.StudentRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DetailsStudentDto>> GetAllBySubject(DetailsSubject details)
        {

            IQueryable<DetailsStudentDto> getStudent = from student in _context.Students
                                                        join detail in _context.DetailsSubjects
                                                        on student.Id equals detail.IdStudent
                                                        join subject in _context.Subjects on detail.IdSubject equals subject.Id where subject.Id == details.IdSubject
                                                        select new DetailsStudentDto
                                                        {
                                                            IdStudent = student.Id,
                                                            FirstName = student.FirstName,
                                                            LastName = student.LastName,
                                                            Email = student.Email,
                                                            Subject = subject.Name,
                                                            IdSubject = subject.Id
                                                        };

            if (!getStudent.Any(x => x.IdSubject == details.IdSubject))
            {
                throw new BusinessException("this subject does not exist");
            }

            return getStudent;
        }

    }
}
