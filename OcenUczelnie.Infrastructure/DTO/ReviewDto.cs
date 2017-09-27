using System;

namespace OcenUczelnie.Infrastructure.DTO
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
    }
}