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
        Task RemoveAsync(Guid id);
        Task UpdateAsync(User user);
        Task<User> GetByIdAsync(Guid id, bool getReviews = false, bool getOpinions = false);
        Task<User> GetByEmailAsync(string email, bool getReviews = false, bool getOpinions = false);
        Task<User> GetByNameAsync(string name, bool getReviews = false, bool getOpinions = false);
        Task<IEnumerable<User>> BrowseAllAsync(bool getReviews = false, bool getOpinions = false);
        Task ConfirmUser(Guid userId);
    }
}