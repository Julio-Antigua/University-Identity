﻿using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public IEnumerable<CourseDto> GetAllCourse()
        {
            IEnumerable<Course> courseList = _unitOfWork.CourseRepository.GetAll();
            IEnumerable<CourseDto> courseDtoList = _mapper.Map<IEnumerable<CourseDto>>(courseList);
            return courseDtoList;
        }
        public async Task<CourseDto> GetById(int id)
        {
            Course course = await _unitOfWork.CourseRepository.GetById(id);
            if (course == null)
            {
                throw new BusinessException("This course doesn`t exits");
            }
            CourseDto courseDto = _mapper.Map<CourseDto>(course);
            return courseDto;
        }
        public async Task<Course> Add(CourseDto courseDto)
        {
            Course course = _mapper.Map<Course>(courseDto);
            await _unitOfWork.CourseRepository.Add(course);
            await _unitOfWork.SaveChangesAsync();
            return course;
        }
        public async Task<bool> UpdateById(int id, CourseDto courseDto)
        {
            Course course = _mapper.Map<Course>(courseDto);
            if (course.Id != id)
            {
                return false;
                throw new BusinessException("this course doesn`t exist");
            }
            else
            {
                course.Id = id;
                _unitOfWork.CourseRepository.UpdateById(course);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteById(int id)
        {
            Course course = await _unitOfWork.CourseRepository.GetById(id);
            if(course == null)
            {
                return false;
                throw new BusinessException("This course doesn`t exits");
            }
            else
            {
                await _unitOfWork.CourseRepository.DeleteById(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }
    }
}
