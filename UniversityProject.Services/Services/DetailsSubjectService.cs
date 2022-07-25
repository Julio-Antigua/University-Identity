using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Services.Services
{
    public class DetailsSubjectService : IDetailsSubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetailsSubjectService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public IEnumerable<DetailsSubjectDto> GetAll()
        {
            IEnumerable<DetailsSubject> details = _unitOfWork.DetailsSubjectRepository.GetAll();
            IEnumerable<DetailsSubjectDto> detailsList = _mapper.Map<IEnumerable<DetailsSubjectDto>>(details);
            return detailsList;
        }

        public IEnumerable<DetailsSubjectDto> GetByIdStudent(int id)
        {
            IEnumerable<DetailsSubject> details = _unitOfWork.DetailsSubjectRepository.GetSubjectByStudent(id);
            IEnumerable<DetailsSubjectDto> detailsDto = _mapper.Map<IEnumerable<DetailsSubjectDto>>(details);
            return detailsDto;
        }

        public IEnumerable<DetailsSubjectDto> GetByIdSubject(int id)
        {
            IEnumerable<DetailsSubject> details = _unitOfWork.DetailsSubjectRepository.GetStudentBySubject(id);
            IEnumerable<DetailsSubjectDto> detailsDto = _mapper.Map<IEnumerable<DetailsSubjectDto>>(details);
            return detailsDto;
        }

        public async Task<bool> DeleteByIdStudent(int idStudent, int idSubject)
        {
            await _unitOfWork.DetailsSubjectRepository.DeleteByIdStudent(idStudent,idSubject);    
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
