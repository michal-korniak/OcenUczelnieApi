using System;

namespace OcenUczelnie.Core.Domain {
    public class ReviewUserDisapproved {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ReviewId { get; set; }
        public Review Review { get; set; }

        public ReviewUserDisapproved (Guid userId, Guid reviewId) {
            UserId = userId;
            ReviewId = reviewId;
        }
        protected ReviewUserDisapproved () {

        }
    }
}