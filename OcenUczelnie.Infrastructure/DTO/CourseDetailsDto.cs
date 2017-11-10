using System;
using OcenUczelnie.Core.Domain;
using System.Collections.Generic;

namespace OcenUczelnie.Infrastructure.DTO
{
    public class CourseDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public UniversityDto University { get; set; }
        public ICollection<ReviewDto> Reviews { get; set; }
        public double AvgRating { get; set; }
        public int CountRating { get; set; }


    }
}