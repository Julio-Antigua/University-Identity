using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(s => s.DateOfBirth, c => c.MapFrom(x => x.DateOfBirth))
                .ForMember(s => s.EntryDate, c => c.MapFrom(x => x.CreationDate))
                .ReverseMap()
                .ForMember(x => x.CreationDate, c => c.MapFrom(x => Convert.ToDateTime(DateTime.Now)))
                .ForMember(s => s.DateOfBirth, c => c.MapFrom(x => Convert.ToDateTime(x.DateOfBirth)))
                .ForMember(s => s.UpdateDate, c => c.MapFrom(x => Convert.ToDateTime(DateTime.Now)))
                .ForMember(student => student.DetailsSubjects, options => options.MapFrom(MapDetailsSubject));

            CreateMap<Course, CourseDto>()
                .ReverseMap()
                .ForMember(s => s.CreationDate, c => c.MapFrom(x => Convert.ToDateTime(DateTime.Now)))
                .ForMember(s => s.UpdateDate, c => c.MapFrom(x => Convert.ToDateTime(DateTime.Now)));

            CreateMap<Subject, SubjectDto>()
                .ReverseMap()
                .ForMember(s => s.CreationDate, c => c.MapFrom(x => Convert.ToDateTime(DateTime.Now)))
                .ForMember(s => s.UpdateDate, c => c.MapFrom(x => Convert.ToDateTime(DateTime.Now)));

            CreateMap<DetailsSubject,DetailsSubjectDto>().ReverseMap();

        }

        private List<Domain.Entities.DetailsSubject> MapDetailsSubject(StudentDto studentDto, Student student)
        {
            List<Domain.Entities.DetailsSubject> result = new List<Domain.Entities.DetailsSubject>();
            if (studentDto.SubjectIds == null) { return result; }
            foreach (var subjectId in studentDto.SubjectIds)
            {
                result.Add(new Domain.Entities.DetailsSubject() { IdSubject = subjectId });
            }
            return result;
        }
    }
}
