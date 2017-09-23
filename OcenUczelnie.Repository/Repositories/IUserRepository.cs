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
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByNameAsync(string name);
        Task<IEnumerable<User>> BrowseAllAsync();
    }
}