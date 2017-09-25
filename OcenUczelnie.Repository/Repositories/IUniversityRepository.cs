using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Core.Repositories
{
    public interface IUniversityRepository: IRepository
    {
        Task AddAsync(Univeristy univeristy);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Univeristy univeristy);
        Task<Univeristy> GetByIdAsync(Guid id);
        Task<IEnumerable<Univeristy>> BrowseAllAsync();
    }
}