using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.EF;

namespace OcenUczelnie.Infrastructure.Repositories
{
    public class UniveristyRepository: IUniversityRepository
    {
        private readonly OcenUczelnieContext _context;

        public UniveristyRepository(OcenUczelnieContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Univeristy univeristy)
        {
            await _context.Universities.AddAsync(univeristy);
            await _context.SaveChangesAsync();

        }

        public async Task RemoveAsync(Guid id)
        {
            var university = await GetByIdAsync(id);
            if (university == null)
                return;
            _context.Remove(university);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Univeristy univeristy)
        {
            _context.Update(univeristy);
            await _context.SaveChangesAsync();
        }

        public async Task<Univeristy> GetByIdAsync(Guid id)
            => await _context.Universities.SingleOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<Univeristy>> BrowseAllAsync()
            => await _context.Universities.ToListAsync();
    }
}