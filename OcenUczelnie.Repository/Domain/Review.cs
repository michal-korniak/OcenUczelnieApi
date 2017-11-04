using System;

namespace OcenUczelnie.Core.Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }

        public Review(Guid id, User user, Course course, int rating, string content)
        {
            Id = id;
            User = user;
            Course = course;
            Rating = rating;
            Content = content;
        }

        protected Review()
        {
            
        }
    }
}