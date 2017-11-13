using System;
using System.Linq;
using System.Threading.Tasks;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.EF;

namespace OcenUczelnie.Infrastructure.Repositories
{
    public class ReviewOpinionRepository : IReviewOpinionRepository
    {
        private readonly OcenUczelnieContext _context;

        public ReviewOpinionRepository(OcenUczelnieContext context)
        {
            _context = context;
        }

        public async Task AddApproveAsync(Guid userId, Guid reviewId)
        {
            var reviewUserApproved = new ReviewUserApproved(userId, reviewId);
            _context.ReviewUserApproved.Add(reviewUserApproved);
            await _context.SaveChangesAsync();
        }

        public async Task AddDisapproveAsync(Guid userId, Guid reviewId)
        {
            var reviewUserDisapproved = new ReviewUserDisapproved(userId, reviewId);
            _context.ReviewUserDisapproved.Add(reviewUserDisapproved);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveApproveAsync(Guid userId, Guid reviewId)
        {
            var reviewUserApproved = new ReviewUserApproved(userId, reviewId);
            _context.ReviewUserApproved.Remove(reviewUserApproved);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDisapproveAsync(Guid userId, Guid reviewId)
        {
            var reviewUserDisapproved = new ReviewUserDisapproved(userId, reviewId);
            _context.ReviewUserDisapproved.Remove(reviewUserDisapproved);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAllOpinions(Guid reviewId)
        {
            _context.RemoveRange(_context.ReviewUserApproved.Where(r => r.ReviewId == reviewId));
            _context.RemoveRange(_context.ReviewUserDisapproved.Where(r => r.ReviewId == reviewId));
            await _context.SaveChangesAsync();
        }

        public int GetUserOpinion(Guid userId, Guid reviewId)
        {
            if (_context.ReviewUserApproved.Count(rua => rua.ReviewId == reviewId && rua.UserId == userId) == 1)
                return 1;
            if (_context.ReviewUserDisapproved.Count(rud => rud.ReviewId == reviewId && rud.UserId == userId) == 1)
                return -1;
            return 0;
        }
    }
}