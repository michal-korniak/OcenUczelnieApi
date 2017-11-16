using System;
using System.Collections.Generic;

namespace OcenUczelnie.Core.Domain
{
    public class Review
    {
        public Review(Guid id, Guid userId, Guid courseId, int rating, string content)
        {
            Id = id;
            UserId = userId;
            CourseId = courseId;
            Rating = rating;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }


        protected Review()
        {
        }

        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<ReviewUserApproved> ReviewUserApproved { get; set; }
        public ICollection<ReviewUserDisapproved> ReviewUserDisapproved { get; set; }
    }
}