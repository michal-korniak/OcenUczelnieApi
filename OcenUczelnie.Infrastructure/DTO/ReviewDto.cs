using System;

namespace OcenUczelnie.Infrastructure.DTO
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }

        public UserDto User { get; set; }
        public CourseDto Course { get; set; }
    }
}