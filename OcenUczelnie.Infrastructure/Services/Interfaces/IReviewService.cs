using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IReviewService: IService
    {
        Task PostReview(Guid userId, Guid courseId, int rating, string content);
        Task<IEnumerable<ReviewDto>> GetReviews(Guid courseId);
        Task ApproveReview(Guid userId,Guid reviewId);
        Task DisapproveReview(Guid userId, Guid reviewId);
        int GetReviewPoints(Guid reviewId);
        int GetUserMarkToReview(Guid userId, Guid reviewId);
        Task DeleteApproveFromReview(Guid userId, Guid reviewId);
        Task DeleteDisapproveFromReview(Guid userId, Guid reviewId);
    }
}