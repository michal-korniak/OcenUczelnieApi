using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper _mapper;
        private readonly IUniversityRepository _universityRepository;

        public UniversityService(IUniversityRepository universityRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _universityRepository = universityRepository;
        }
        public async Task AddAsync(string name, string place, string imagePath)
        {
            var univeristy=new University(Guid.NewGuid(), name, place,imagePath);
            await _universityRepository.AddAsync(univeristy);
        }

        public async Task<UniversityDto> GetAsync(Guid id)
        {
            var university = await _universityRepository.GetByIdAsync(id);
            return _mapper.Map<University, UniversityDto>(university);
        }

        public async Task<UniversityDetailsDto> GetDetailsAsync(Guid id)
        {
            var university = await _universityRepository.GetByIdAsync(id,true);
            university.Courses = university.Courses.OrderBy(c => c.Name).ToList();
            var universityDetailsDto= _mapper.Map<University, UniversityDetailsDto>(university);
            universityDetailsDto.Departments = await _universityRepository.GetDepartamentNamesAsync(id);
            return universityDetailsDto;
        }

        public async Task<IEnumerable<UniversityDto>> BrowseAllAsync()
        {
            var universites =await _universityRepository.BrowseAllAsync();
            universites = universites.OrderBy(u => u.Name);
            return _mapper.Map<IEnumerable<University>, IEnumerable<UniversityDto>>(universites);
        }
    }
}