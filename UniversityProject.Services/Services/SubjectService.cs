using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Services.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UniversityContext _context;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper, UniversityContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        public IEnumerable<SubjectDto> GetAll()
        {
            IEnumerable<Subject> subject = _unitOfWork.SubjectRepository.GetAll();
            IEnumerable<SubjectDto> result = _mapper.Map<IEnumerable<SubjectDto>>(subject);
            return result;
        }
        public async Task<SubjectDto> GetById(int id)
        {
           Subject subject = await _unitOfWork.SubjectRepository.GetById(id);
           if(subject == null)
            {
                throw new BusinessException("This subject doesn`t exist");
            }
           SubjectDto subjectDto = _mapper.Map<SubjectDto>(subject);   
           return subjectDto;  
        }

   
        public async Task Add(SubjectDto subjectDto)
        {
            Subject subject = _mapper.Map<Subject>(subjectDto);
            await _unitOfWork.SubjectRepository.Add(subject);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> UpdateById(int id,SubjectDto subjectDto)
        {
            Subject subject = _mapper.Map<Subject>(subjectDto);
            if (subject.Id != id)
            {
                throw new BusinessException("This subject doesn`t exist");
            }
            _unitOfWork.SubjectRepository.UpdateById(subject);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            Subject subject = await _unitOfWork.SubjectRepository.GetById(id);
            if( subject == null)
            {
                throw new BusinessException("This subject doesn`t exist");
            }
            await _unitOfWork.SubjectRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DetailsStudentDto>> GetAllBySubject(DetailsSubject details)
        {
            IQueryable<DetailsStudentDto> getSubject = from subject in _context.Subjects
                                                       join detail in _context.DetailsSubjects
                                                       on subject.Id equals detail.IdSubject
                                                       join student in _context.Students
                                                       on detail.IdStudent equals student.Id
                                                       where student.Id == details.IdStudent
                                                       select new DetailsStudentDto
                                                       {
                                                           IdStudent = student.Id,
                                                           FirstName = student.FirstName,
                                                           LastName = student.LastName,
                                                           Email = student.Email,
                                                           Subject = subject.Name,
                                                           IdSubject = subject.Id
                                                       };
            if (!getSubject.Any(x => x.IdStudent == details.IdStudent))
            {
                throw new BusinessException("this subject does not exist");
            }
            return getSubject;
        }
    }
}
