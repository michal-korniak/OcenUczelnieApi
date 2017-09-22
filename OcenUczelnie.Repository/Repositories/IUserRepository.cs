using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Core.Repositories
{
    public interface IUserRepository: IRepository
    {
        Task AddAsync(User user);
        Task RemoveAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetAsync(Guid id);
        Task<IEnumerable<User>> BrowseAllAsync();
    }
}