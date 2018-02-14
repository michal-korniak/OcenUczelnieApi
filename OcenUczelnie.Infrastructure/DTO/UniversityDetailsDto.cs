using System;
using System.Collections.Generic;

namespace OcenUczelnie.Infrastructure.DTO
{
    public class UniversityDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Shortcut {get;set;}
        public string Place { get; set; }
        public string ImagePath { get; set; }
        public ICollection<string> Departments { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}