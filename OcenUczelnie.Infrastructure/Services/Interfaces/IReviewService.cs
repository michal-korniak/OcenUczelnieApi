using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IReviewService: IService
    {
        Task PostReviewAsync(Guid userId, Guid courseId, int rating, string content);
        Task RemoveReviewAsync(Guid userId, Guid reviewId);
        Task<IEnumerable<ReviewDto>> GetReviewsAsync(Guid courseId);
        Task ApproveReviewAsync(Guid userId,Guid reviewId);
        Task DisapproveReviewAsync(Guid userId, Guid reviewId);
        Task DeleteApproveAsync(Guid userId, Guid reviewId);
        Task DeleteDisapproveAsync(Guid userId, Guid reviewId);
        int GetUserOpinionToReview(Guid userId, Guid reviewId);

    }
}