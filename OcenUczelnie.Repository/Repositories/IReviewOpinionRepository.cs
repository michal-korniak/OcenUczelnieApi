using System;
using System.Threading.Tasks;

namespace OcenUczelnie.Core.Repositories
{
    public interface IReviewOpinionRepository : IRepository
    {
        Task AddApproveAsync(Guid userId, Guid reviewId);
        Task AddDisapproveAsync(Guid userId, Guid reviewId);
        Task RemoveApproveAsync(Guid userId, Guid reviewId);
        Task RemoveDisapproveAsync(Guid userId, Guid reviewId);
        Task RemoveAllOpinions(Guid reviewId);
        int GetUserOpinion(Guid userId, Guid reviewId);
    }
}