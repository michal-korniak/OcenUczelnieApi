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
    public class UserRepository: IUserRepository
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
            if (user == null)
                return;
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email==email);
        }
        public async Task<User> GetByNameAsync(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<User>> BrowseAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

    }
}