using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.EF;

namespace OcenUczelnie.Infrastructure.Repositories
{
    public class ReviewRepository: IReviewRepository
    {
        private readonly OcenUczelnieContext _context;
        private readonly IUserRepository _userRepository;

        public ReviewRepository(OcenUczelnieContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var review =await GetByIdAsync(id);
            if(review==null)
                return;
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            var review=await _context.Reviews.Include(c=>c.User).
                SingleOrDefaultAsync(c => c.Id == id);
            return review;
        }

        public async Task<ICollection<Review>> GetReviewsForCourse(Guid courseId)
        {
            var reviews = await _context.Reviews.Include(c => c.User)
                .Include(c=>c.ReviewUserApproved).ThenInclude(rua=>rua.Review)
                .Include(c => c.ReviewUserApproved).ThenInclude(rua => rua.User)
                .Include(c => c.ReviewUserDisapproved).ThenInclude(rua => rua.Review)
                .Include(c => c.ReviewUserDisapproved).ThenInclude(rua => rua.User)
                .Where(r => r.Course.Id == courseId).ToListAsync();
            return reviews;
        }

        public async Task AddUserReviewApproved(Guid userId, Guid reviewId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var review = await GetByIdAsync(reviewId);
            var reviewUserApproved = new ReviewUserApproved(user, review);
            _context.ReviewUserApproved.Add(reviewUserApproved);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveUserReviewApproved(Guid userId, Guid reviewId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var review = await GetByIdAsync(reviewId);
            var reviewUserApproved = new ReviewUserApproved(user, review);
            _context.ReviewUserApproved.Remove(reviewUserApproved);
            await _context.SaveChangesAsync();
        }
        public async Task AddUserReviewDisapproved(Guid userId, Guid reviewId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var review = await GetByIdAsync(reviewId);
            var reviewUserDisapproved = new ReviewUserDisapproved(user, review);
            _context.ReviewUserDisapproved.Add(reviewUserDisapproved);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveUserReviewDisapproved(Guid userId, Guid reviewId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var review = await GetByIdAsync(reviewId);
            var reviewUserDisapproved = new ReviewUserDisapproved(user, review);
            _context.ReviewUserDisapproved.Remove(reviewUserDisapproved);
            await _context.SaveChangesAsync();
        }
        public int GetReviewPoints(Guid reviewId)
        {
            int approved=_context.ReviewUserApproved.Where(rua => rua.ReviewId==reviewId).Count();
            int disapproved= _context.ReviewUserDisapproved.Where(rud => rud.ReviewId == reviewId).Count();
            return approved - disapproved;
        }
        public int GetUserMarkToReview(Guid userId, Guid reviewId)
        {
            if (_context.ReviewUserApproved.Where(rua => rua.ReviewId == reviewId && rua.UserId == userId).Count() == 1)
                return 1;
            if (_context.ReviewUserDisapproved.Where(rud => rud.ReviewId == reviewId && rud.UserId == userId).Count() == 1)
                return -1;
            else
                return 0;
        }
    }
}