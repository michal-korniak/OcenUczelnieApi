using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.Command;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IUniversityService: IService
    {
        Task AddAsync (string name, string shortcut, string place, string base64Image);
        Task<UniversityDto> GetAsync(Guid id);
        Task<UniversityDetailsDto> GetDetailsAsync(Guid id);
        Task<IEnumerable<UniversityDto>> BrowseAllAsync();
        Task UpdateCoursesAsync(Guid id, ICollection<UpdateCourses.Course> courses);
    }
}