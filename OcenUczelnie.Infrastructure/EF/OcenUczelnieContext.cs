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
        public DbSet<ReviewUserApproved> ReviewUserApproved { get; set; }
        public DbSet<ReviewUserDisapproved> ReviewUserDisapproved { get; set; }


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


            var reviewEntity = modelBuilder.Entity<Review>();
            reviewEntity.HasKey(r => r.Id);
            reviewEntity.Property(r => r.Rating).IsRequired();
            reviewEntity.Property(r => r.Content).IsRequired();

            var courseEntity = modelBuilder.Entity<Course>();
            courseEntity.HasKey(c => c.Id);
            courseEntity.Property(c => c.Name).IsRequired();
            courseEntity.Property(c => c.Department).IsRequired();

            var univerEntity = modelBuilder.Entity<University>();
            univerEntity.HasKey(u => u.Id);
            univerEntity.Property(u => u.Name).IsRequired();
            univerEntity.Property(u => u.Id).IsRequired();
            univerEntity.Property(u => u.Place).IsRequired();
            univerEntity.Property(u => u.ImagePath).IsRequired();


            reviewEntity.HasOne(r => r.User).WithMany(u => u.Reviews).IsRequired();;
            reviewEntity.HasOne(r => r.Course).WithMany(c => c.Reviews).IsRequired();
            courseEntity.HasOne(c => c.University).WithMany(u => u.Courses).IsRequired();

            modelBuilder.Entity<ReviewUserApproved>().HasKey(ru => new { ru.ReviewId, ru.UserId });
            modelBuilder.Entity<ReviewUserApproved>()
                .HasOne(ru => ru.Review)
                .WithMany(ru => ru.ReviewUserApproved)
                .HasForeignKey(ru => ru.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReviewUserApproved>()
                .HasOne(ru => ru.User)
                .WithMany(ru => ru.ReviewUserApproved)
                .HasForeignKey(ru => ru.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ReviewUserDisapproved>().HasKey(ru => new { ru.ReviewId, ru.UserId });
            modelBuilder.Entity<ReviewUserDisapproved>()
                .HasOne(ru => ru.Review)
                .WithMany(ru => ru.ReviewUserDisapproved)
                .HasForeignKey(ru => ru.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReviewUserDisapproved>()
                .HasOne(ru => ru.User)
                .WithMany(ru => ru.ReviewUserDisapproved)
                .HasForeignKey(ru => ru.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}