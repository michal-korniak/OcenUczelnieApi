using System;

namespace OcenUczelnie.Infrastructure.DTO
{
    public class UniversityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string ImagePath { get; set; }
    }
}