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
    public class UserRepository : IUserRepository
    {
        private readonly OcenUczelnieContext _context;

        public UserRepository(OcenUczelnieContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<User>> BrowseAllAsync(bool getReviews = false, bool getOpinions = false)
        {
            var query = GetUserQuery(getReviews, getOpinions);
            var users = await query.ToListAsync();
            return users;
        }

        public async Task<User> GetByIdAsync(Guid id, bool getReviews = false, bool getOpinions = false)
        {
            var query = GetUserQuery(getReviews, getOpinions);
            var user = await query.SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> GetByNameAsync(string name, bool getReviews = false, bool getOpinions = false)
        {
            var query = GetUserQuery(getReviews, getOpinions);
            var user = await query.SingleOrDefaultAsync(x => x.Name == name);
            return user;
        }

        public async Task<User> GetByEmailAsync(string email, bool getReviews = false, bool getOpinions = false)
        {
            var query = GetUserQuery(getReviews, getOpinions);
            var user = await query.SingleOrDefaultAsync(x => x.Email == email);
            return user;
        }

        private IQueryable<User> GetUserQuery(bool getReviews, bool getOpinions)
        {
            IQueryable<User> query= _context.Users;
            if (getReviews)
                query = query.Include(u => u.Reviews);
            else if (getOpinions)
                query = query
                    .Include(c => c.ReviewUserApproved).ThenInclude(rua => rua.Review)
                    .Include(c => c.ReviewUserApproved).ThenInclude(rua => rua.User)
                    .Include(c => c.ReviewUserDisapproved).ThenInclude(rua => rua.Review)
                    .Include(c => c.ReviewUserDisapproved).ThenInclude(rua => rua.User);
            return query;
        }
    }
}