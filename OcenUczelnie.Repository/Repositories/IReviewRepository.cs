using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Core.Repositories
{
    public interface IReviewRepository: IRepository
    {
        Task AddAsync(Review review);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Review review);
        Task<Review> GetByIdAsync(Guid id);
        Task<ICollection<Review>> GetReviewsForCourse(Guid courseId);
        Task AddUserReviewApproved(Guid userId, Guid reviewId);
        Task AddUserReviewDisapproved(Guid userId, Guid reviewId);
        Task RemoveUserReviewApproved(Guid userId, Guid reviewId);
        Task RemoveUserReviewDisapproved(Guid userId, Guid reviewId);
        int GetReviewPoints(Guid reviewId);
        int GetUserMarkToReview(Guid userId, Guid reviewId);
    }
}