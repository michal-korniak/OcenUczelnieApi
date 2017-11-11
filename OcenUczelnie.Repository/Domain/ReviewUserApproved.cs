using OcenUczelnie.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OcenUczelnie.Core.Domain
{
    public class ReviewUserApproved
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ReviewId { get; set; }
        public Review Review { get; set; }

        public ReviewUserApproved(User user, Review review)
        {
            User = user;
            Review = review;
        }
        protected ReviewUserApproved()
        {

        }
    }
}
