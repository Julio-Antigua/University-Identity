using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Domain.CustomEntities;
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
        public IEnumerable<SubjectDto> GetAllSubject()
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

   
        public async Task<Subject> Add(SubjectDto subjectDto)
        {
            Subject subject = _mapper.Map<Subject>(subjectDto);
            await _unitOfWork.SubjectRepository.Add(subject);
            await _unitOfWork.SaveChangesAsync();
            return subject;
        }

        public async Task<bool> UpdateById(int id,SubjectDto subjectDto)
        {
            Subject subject = _mapper.Map<Subject>(subjectDto);
            if (subject.Id != id)
            {
                return false;
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
                return false;
                throw new BusinessException("This subject doesn`t exist");
            }
            await _unitOfWork.SubjectRepository.DeleteById(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public IEnumerable<DetailsStudentDto> GetAllByStudent(DetailsSubject details)
        {
            IEnumerable<DetailsStudent> student = _unitOfWork.StudentRepository.GetAllByStudent(details);
            IEnumerable<DetailsStudentDto> studentDto = _mapper.Map<IEnumerable<DetailsStudentDto>>(student);
            return studentDto;
        }
    }
}
