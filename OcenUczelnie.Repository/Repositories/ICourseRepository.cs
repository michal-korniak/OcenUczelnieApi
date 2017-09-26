using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Core.Repositories
{
    public interface ICourseRepository: IRepository
    {
        Task AddAsync(Course course);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Course course);
        Task<Course> GetByIdAsync(Guid id);
        Task<IEnumerable<Course>> BrowseUniversityCoursesAsync(Guid universityId);
    }
}