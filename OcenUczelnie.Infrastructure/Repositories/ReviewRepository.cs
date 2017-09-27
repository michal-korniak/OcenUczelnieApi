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
            var review=await _context.Reviews.SingleOrDefaultAsync(c => c.Id == id);
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsForCourse(Guid courseId)
        {
            var reviews = await _context.Reviews.Where(r => r.CourseId == courseId).ToListAsync();
            return reviews;
        }
    }
}