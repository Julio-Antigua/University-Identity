using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Domain.Options;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Services.Services
{
    public class StudentService : IStudentService   
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly PaginationOptions _paginationOptions;


        public StudentService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options,IMapper mapper, IUriService uriService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uriService = uriService;
            _paginationOptions = options.Value;

        }
        public (PagedList<Student>,Metadata) GetAll(StudentQueryFilter filter)
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
            int nextPage = filter.PageNumber >= 1 && filter.PageNumber < pageStudent.TotalPages ? filter.PageNumber + 1 : 1;
            int prevPage = filter.PageNumber - 1 >= 1 && filter.PageNumber <= pageStudent.TotalPages ? filter.PageNumber - 1 : 1;
            Metadata metadata = new Metadata
            {
                TotalCount = pageStudent.TotalCount,
                PageSize = pageStudent.PageSize,
                CurrentPage = pageStudent.CurrentPage,
                TotalPage = pageStudent.TotalPages,
                HasNextPage = pageStudent.HasNextPage,
                HasPreviousPage = pageStudent.HasPreviousPage,
                NextPageUrl = _uriService.GetStudentPaginationUri(filter, nextPage, "/api/Student").ToString(),
                PreviousPageUrl = _uriService.GetStudentPaginationUri(filter, prevPage, "/api/Student").ToString()
            };
            return (pageStudent,metadata);
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
            IEnumerable<int> subjectIds = _unitOfWork.StudentRepository.GetAll().Where(x => studentsDto.SubjectIds.Contains(x.Id)).Select(x => x.Id).ToList();
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

        public IEnumerable<DetailsStudentDto> GetAllBySubject(DetailsSubject details)
        {
            IEnumerable<DetailsStudent>  student = _unitOfWork.StudentRepository.GetAllBySubject(details);
            IEnumerable<DetailsStudentDto> studentDto = _mapper.Map<IEnumerable<DetailsStudentDto>>(student);
            return studentDto;
        }

    }
}
