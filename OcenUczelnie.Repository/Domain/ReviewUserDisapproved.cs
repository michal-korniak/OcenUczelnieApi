using System;

namespace OcenUczelnie.Core.Domain
{
    public class ReviewUserDisapproved
    {
            public Guid UserId { get; set; }
            public User User { get; set; }

            public Guid ReviewId { get; set; }
            public Review Review { get; set; }

        public ReviewUserDisapproved(User user, Review review)
        {
            User = user;
            Review = review;
        }
        protected ReviewUserDisapproved()
        {

        }
    }
}