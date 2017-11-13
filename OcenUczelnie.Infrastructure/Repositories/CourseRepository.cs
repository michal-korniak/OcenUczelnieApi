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
    public class CourseRepository: ICourseRepository
    {
        private readonly OcenUczelnieContext _context;

        public CourseRepository(OcenUczelnieContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var course =await GetByIdAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Course>> BrowseCoursesForUniversityAsync(Guid universityId, bool getUniversity=false, bool getReviews = false)
        {
            var courses = await _context.Courses.Where(c => c.University.Id == universityId).OrderBy(c => c.Name).ToListAsync();
            return courses;
        }

        public async Task<Course> GetByIdAsync(Guid id, bool getUniversity=false,bool getReviews=false)
        {
            var query = GetCourseQuery(getUniversity,getReviews);
            var course = await query.SingleOrDefaultAsync(c => c.Id == id);
            
            return course;
        }

        private IQueryable<Course> GetCourseQuery(bool getUniversity,bool getReviews)
        {
            IQueryable<Course> query = _context.Courses;
            if(getUniversity)
                query=query.Include(c => c.University);
            if (getReviews)
                query = query.Include(c => c.Reviews);
            return query;
        }


    }
}