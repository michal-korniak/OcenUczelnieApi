using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Infrastructure.Services
{
    public class CourseService: ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task<CourseDto> Get(Guid id)
        {
            var course=await _courseRepository.GetByIdAsync(id);
            return _mapper.Map<Course, CourseDto>(course);
        }

        public async Task<IEnumerable<CourseDto>> BrowseUniversityCoursesAsync(Guid universityId)
        {
            var courses = await _courseRepository.BrowseUniversityCoursesAsync(universityId);
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDetailsDto> GetDetails(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            course.Reviews = await _reviewRepository.GetReviewsForCourse(id);
            var courseDetailsDto = _mapper.Map<Course, CourseDetailsDto>(course);

            return courseDetailsDto;

        }
    }
}