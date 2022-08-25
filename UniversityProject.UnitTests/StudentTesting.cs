using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityProject.Api.Controllers;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Options;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Infrastructure.UnitOfWork;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.QueryFilters;
using UniversityProject.Services.Services;
using Xunit;

namespace UniversityProject.UnitTests
{
    public class StudentTesting
    {
        private readonly StudentController _studentController;
        private readonly IStudentService _studentService;

        private readonly CourseController _courseController;
        private readonly ICourseService _courseService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly PaginationOptions _paginationOptions;
        private readonly UniversityContext _context;

        private readonly StudentQueryFilter _filter;

        private readonly IOptions<PaginationOptions> options;

        public StudentTesting()
        {
            //var mockStudent = new Mock<IStudentService>();
            //_studentService = new StudentService(_unitOfWork, (IOptions<PaginationOptions>)_paginationOptions, _mapper, _uriService, _context);

            //_studentController = new StudentController(mockStudent.Object);

           
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            StudentQueryFilter search = null;

            var metada = new Metadata() 
            {
                TotalCount = 1,
                PageSize = 1,
                CurrentPage = 1,
                TotalPage = 1,
                HasNextPage = false,
                HasPreviousPage = false,
                NextPageUrl = "",
                PreviousPageUrl = ""
            };

            var list = new List<int>();
            list.Add(1);

            var dto = new List<StudentDto>();
            dto.Add(new StudentDto() { Id = 1, FirstName = "Juan", LastName = "Payano", Email = "juan@gmail.com", DateOfBirth = DateTime.Now, EntryDate = DateTime.Now });
            

            var page = new PagedList<StudentDto>(dto,1,1,1);

            Mock<IStudentService> mockStudentService = new Mock<IStudentService>();
            mockStudentService.Setup(x => x.GetAllStudent(search)).Returns((page,metada));

            StudentController studentController = new StudentController(mockStudentService.Object);


            // Act
            OkObjectResult result = (OkObjectResult) studentController.GetAll(search);


            // Assert
            Page<StudentDto> student = Assert.IsType<Page<StudentDto>>(result);
            Assert.True(student.Items.Count > 0);
        }

        //[Fact]
        //public async void Test2()
        //{
        //    // Arrange
        //    int id = 1;

        //    // Act
        //    var result = (OkObjectResult) await _studentController.GetById(id);

        //    // Assert
        //    var student = Assert.IsType<StudentDto>(result?.Value);
        //    Assert.True(student != null);
        //    Assert.Equal(student?.Id, id);
        //}

        [Fact]
        public void Test3()
        {
            // Arrange
            var dto = new List<CourseDto>();
            dto.Add(new CourseDto() { Id = 1, Name = "AX-05" });

            Mock<ICourseService> mockCourseService = new Mock<ICourseService>();
            mockCourseService.Setup(x => x.GetAllCourse()).Returns(dto);

            CourseController courseController = new CourseController(mockCourseService.Object);

            // Act
            OkObjectResult result = (OkObjectResult) courseController.GetAll();

            // Assert
            ApiResponse<IEnumerable<CourseDto>> course = Assert.IsType<ApiResponse<IEnumerable<CourseDto>>>(result.Value);
            Assert.True(course.Data.Count() > 0);
        }
    }
}
