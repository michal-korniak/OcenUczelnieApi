using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Infrastructure.EF
{
    public class OcenUczelnieContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<Course> Courses { get; set; }



        public OcenUczelnieContext(DbContextOptions options) : base(options)
        {
        }

    }
}