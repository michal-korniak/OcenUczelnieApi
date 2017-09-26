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
    class UniversityService : IUniversityService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IUniversityRepository _universityRepository;

        public UniversityService(IUniversityRepository universityRepository,
            ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _universityRepository = universityRepository;
        }
        public async Task AddAsync(string name, string place)
        {
            var univeristy = await _universityRepository.GetByNameAsync(name);
            if(univeristy!=null)
                throw new Exception("University was already added");
            univeristy=new University(Guid.NewGuid(), name, place);
            await _universityRepository.AddAsync(univeristy);
        }

        public async Task<UniversityDto> Get(Guid id)
        {
            var university = await _universityRepository.GetByIdAsync(id);
            return _mapper.Map<University, UniversityDto>(university);
        }

        public async Task<IEnumerable<UniversityDto>> BrowseAllAsync()
        {
            var universites =await _universityRepository.BrowseAllAsync();
            return _mapper.Map<IEnumerable<University>, IEnumerable<UniversityDto>>(universites);

        }

        public async Task<IEnumerable<CourseDto>> BrowseCoursesAsync(Guid id)
        {
            var courses=await _courseRepository.BrowseUniversityCoursesAsync(id);
            return _mapper.Map<IEnumerable<Course>, IEnumerable<CourseDto>>(courses);
        }
    }
}