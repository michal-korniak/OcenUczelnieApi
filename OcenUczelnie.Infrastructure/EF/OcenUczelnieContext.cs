using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using OcenUczelnie.Core.Domain;

namespace OcenUczelnie.Infrastructure.EF
{
    public class OcenUczelnieContext : DbContext
    {
        public OcenUczelnieContext(DbContextOptions<OcenUczelnieContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Course> Courses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userEntity=modelBuilder.Entity<User>();
            userEntity.HasKey(u => u.Id);
            userEntity.Property(u => u.Id).IsRequired();
            userEntity.Property(u => u.Email).IsRequired();
            userEntity.Property(u => u.Name).IsRequired();
            userEntity.Property(u => u.Password).IsRequired();
            userEntity.Property(u => u.Salt).IsRequired();
            userEntity.Property(u => u.Role).IsRequired();

            var univerEntity = modelBuilder.Entity<University>();
            univerEntity.HasKey(u => u.Id);
            univerEntity.Property(u => u.Name).IsRequired();
            univerEntity.Property(u => u.Id).IsRequired();
            univerEntity.Property(u => u.Place).IsRequired();
            univerEntity.Property(u => u.ImagePath).IsRequired();

            var courseEntity = modelBuilder.Entity<Course>();
            courseEntity.HasKey(c => c.Id);
            courseEntity.Property(c => c.Name).IsRequired();
            courseEntity.Property(c => c.Level).IsRequired(false);


            var reviewEntity = modelBuilder.Entity<Review>();
            reviewEntity.HasKey(r => r.Id);
            reviewEntity.Property(r => r.Rating).IsRequired();
            reviewEntity.Property(r => r.Content).IsRequired();

            reviewEntity.HasOne(r => r.User).WithMany(u => u.Reviews).IsRequired();
            userEntity.HasMany(u => u.Reviews).WithOne(r => r.User).IsRequired();

            reviewEntity.HasOne(r => r.Course).WithMany(c => c.Reviews).IsRequired();
            courseEntity.HasMany(c => c.Reviews).WithOne(r => r.Course).IsRequired();

            courseEntity.HasOne(c => c.University).WithMany(u => u.Courses).IsRequired();
            univerEntity.HasMany(u => u.Courses).WithOne(c => c.University).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}