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
    public class UniveristyRepository: IUniversityRepository
    {
        private readonly OcenUczelnieContext _context;

        public UniveristyRepository(OcenUczelnieContext context)
        {
            _context = context;
        }

        public async Task AddAsync(University university)
        {
            await _context.Universities.AddAsync(university);
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

        public async Task UpdateAsync(University university)
        {
            _context.Update(university);
            await _context.SaveChangesAsync();
        }

        public async Task<string[]> GetDepartamentNamesAsync(Guid id)
        {
            var university = await GetByIdAsync(id,true);
            return await _context.Courses.Where(c => c.University == university).GroupBy(c=>c.Department).Select(c => c.Key)
                .ToArrayAsync();
        }

        public async Task<University> GetByIdAsync(Guid id,bool getCourses=false)
        {
            var query = GetUniversityQuery(getCourses);
            return await query.SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<University> GetByNameAsync(string name, bool getCourses = false)
        {
            var query = GetUniversityQuery(getCourses);
            return await query.SingleOrDefaultAsync(u => u.Name == name);
        }

        public async Task<IEnumerable<University>> BrowseAllAsync(bool getCourses = false)
        {
            var query = GetUniversityQuery(getCourses);
            return await query.ToListAsync();
        }

        private IQueryable<University> GetUniversityQuery(bool getCourses)
        {
            IQueryable<University> query = _context.Universities;
            if (getCourses)
                query = query.Include(u => u.Courses);
            return query;
        }


    }
}