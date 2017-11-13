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
    public class ReviewRepository : IReviewRepository
    {
        private readonly OcenUczelnieContext _context;

        public ReviewRepository(OcenUczelnieContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var review = await GetByIdAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Review> GetByIdAsync(Guid id, bool getUser = false, bool getCourse = false,
            bool getOpinions = false)
        {
            var query = GetReviewQuery(getUser, getCourse, getOpinions);
            var review = await query.SingleOrDefaultAsync(c => c.Id == id);
            return review;
        }

        public async Task<ICollection<Review>> GetReviewsForCourseAsync(Guid courseId, bool getUser = false,
            bool getCourse = false, bool getOpinions = false)
        {
            var query = GetReviewQuery(getUser, getCourse, getOpinions);
            var reviews = await query.Where(r => r.Course.Id == courseId).ToListAsync();
            return reviews;
        }

        private IQueryable<Review> GetReviewQuery(bool getUser, bool getCourse, bool getOpinions)
        {
            IQueryable<Review> query = _context.Reviews;
            if (getUser)
                query=query.Include(c => c.User);
            if (getCourse)
                query=query.Include(c => c.Course);
            if (getOpinions)
                query = query
                    .Include(c => c.ReviewUserApproved).ThenInclude(rua => rua.Review)
                    .Include(c => c.ReviewUserApproved).ThenInclude(rua => rua.User)
                    .Include(c => c.ReviewUserDisapproved).ThenInclude(rua => rua.Review)
                    .Include(c => c.ReviewUserDisapproved).ThenInclude(rua => rua.User);
            return query;
        }
    }
}