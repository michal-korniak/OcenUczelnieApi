using System;
using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;
using OcenUczelnie.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain.Exceptions;
using OcenUczelnie.Infrastructure.Command;

namespace OcenUczelnie.Infrastructure.Services {
    class UniversityService : IUniversityService {
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly IUniversityRepository _universityRepository;

        public UniversityService (IUniversityRepository universityRepository,
            IMapper mapper, IStorageService storageService) {
            _mapper = mapper;
            this._storageService = storageService;
            _universityRepository = universityRepository;
        }
        public async Task AddAsync (string name, string shortcut, string place, string base64Image) {
            var univeristy=await _universityRepository.GetByNameAsync(name);
            if(univeristy!=null)
                throw new OcenUczelnieException(ErrorCodes.NameOccupied);
            string imageUrl = await _storageService.UploadImageAndReturnUrlAsync (base64Image, name);
            univeristy = new University (Guid.NewGuid (), name, shortcut, place, imageUrl);
            await _universityRepository.AddAsync (univeristy);
        }

        public async Task<UniversityDto> GetAsync (Guid id) {
            var university = await _universityRepository.GetByIdAsync (id);
            return _mapper.Map<University, UniversityDto> (university);
        }

        public async Task<UniversityDetailsDto> GetDetailsAsync (Guid id) {
            var university = await _universityRepository.GetByIdAsync (id, true);
            university.Courses = university.Courses.OrderBy (c => c.Name).ToList ();
            var universityDetailsDto = _mapper.Map<University, UniversityDetailsDto> (university);
            universityDetailsDto.Departments = await _universityRepository.GetDepartamentNamesAsync (id);
            return universityDetailsDto;
        }

        public async Task<IEnumerable<UniversityDto>> BrowseAllAsync () {
            var universites = await _universityRepository.BrowseAllAsync ();
            universites = universites.OrderBy (u => u.Name);
            return _mapper.Map<IEnumerable<University>, IEnumerable<UniversityDto>> (universites);
        }

        public async Task UpdateCoursesAsync(Guid id, ICollection<UpdateCourses.Course> newCourses)
        {
            var univeristy=await _universityRepository.GetByIdAsync(id,true);
            List<Course> coursesToRemove=new List<Course>();
            foreach(var oldCourse in univeristy.Courses)
            {
                if(newCourses.SingleOrDefault(n=>n.Id==oldCourse.Id)==null)
                {
                    coursesToRemove.Add(oldCourse);
                }
            }
            foreach(var course in coursesToRemove)
            {
                univeristy.Courses.Remove(course);
            }
            foreach(var newCourse in newCourses)
            {
                if(newCourse.Id!=null)
                {
                    var courseToUpdate=univeristy.Courses.SingleOrDefault(c=>c.Id==newCourse.Id);
                    if(courseToUpdate!=null)
                    {
                        courseToUpdate.Name=newCourse.Name;
                        courseToUpdate.Department=newCourse.Department;
                        continue;
                    }
                }
                var course=new Course(Guid.NewGuid(),newCourse.Name,newCourse.Department);
                univeristy.Courses.Add(course);
            }
            await _universityRepository.UpdateAsync(univeristy);
        }

        public async Task UpdateUniversityAsync(Guid id, string name, string shortcut, string place, string base64Image)
        {
            var univeristy=await _universityRepository.GetByIdAsync(id);
            univeristy.Name=name;
            univeristy.Shortcut=shortcut;
            univeristy.Place=place;
            if(base64Image!=null)
            {
                string imageUrl = await _storageService.UploadImageAndReturnUrlAsync (base64Image, name);
                univeristy.ImagePath=imageUrl;
            }
            await _universityRepository.UpdateAsync(univeristy);
        }

        public async Task DeleteUniversityAsync(Guid id)
        {
            await _universityRepository.RemoveAsync(id);
        }
    }
}