using System;

namespace OcenUczelnie.Core.Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }

        public Review(Guid id, Guid userId, Guid courseId, int rating, string content)
        {
            Id = id;
            UserId = userId;
            CourseId = courseId;
            Rating = rating;
            Content = content;
        }

        protected Review()
        {
            
        }
    }
}