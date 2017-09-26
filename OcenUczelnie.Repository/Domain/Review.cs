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

    }
}