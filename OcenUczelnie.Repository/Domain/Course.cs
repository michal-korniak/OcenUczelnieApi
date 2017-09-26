using System;

namespace OcenUczelnie.Core.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public Guid UniversityId { get; protected set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Cycle { get; set; }
        public string Kind { get; set; }
        public int AvgRating { get; set; }
    }
}