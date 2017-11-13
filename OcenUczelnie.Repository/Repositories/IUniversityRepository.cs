using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Core.Repositories
{
    public interface IUniversityRepository: IRepository
    {
        Task AddAsync(University university);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(University university);
        Task<University> GetByIdAsync(Guid id, bool getCourses = false);
        Task<string[]> GetDepartamentNamesAsync(Guid id);
        Task<IEnumerable<University>> BrowseAllAsync(bool getCourses = false);
    }
}