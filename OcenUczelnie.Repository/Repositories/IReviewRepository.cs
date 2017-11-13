using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Core.Repositories
{
    public interface IReviewRepository : IRepository
    {
        Task AddAsync(Review review);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Review review);
        Task<Review> GetByIdAsync(Guid id, bool getUser = false, bool getCourse = false, bool getOpinions = false);

        Task<ICollection<Review>> GetReviewsForCourseAsync(Guid courseId, bool getUser = false, bool getCourse = false,
            bool getOpinions = false);
    }
}