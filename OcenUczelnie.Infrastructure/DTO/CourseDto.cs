using System;

namespace OcenUczelnie.Infrastructure.DTO
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public Guid UniversityId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Cycle { get; set; }
        public string Kind { get; set; }
        public int AvgRating { get; set; }
    }
}