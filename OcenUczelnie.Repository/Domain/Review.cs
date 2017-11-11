using System;
using System.Collections.Generic;

namespace OcenUczelnie.Core.Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }

        public ICollection<ReviewUserApproved> ReviewUserApproved { get; set; }
        public ICollection<ReviewUserDisapproved> ReviewUserDisapproved { get; set; }



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