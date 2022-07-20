using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Services.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
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
    }
}
