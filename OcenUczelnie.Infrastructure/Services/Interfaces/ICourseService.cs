using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface ICourseService:IService
    {
        Task<CourseDto> Get(Guid id);
        Task<IEnumerable<CourseDto>> BrowseUniversityCoursesAsync(Guid universityId);
    }
}