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
        Task<University> GetByIdAsync(Guid id);
        Task<University> GetByNameAsync(string name);
        Task<IEnumerable<University>> BrowseAllAsync();
    }
}