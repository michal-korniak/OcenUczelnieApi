using System;
using System.Collections.Generic;

namespace OcenUczelnie.Core.Domain
{
    public class University
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public string Place { get; set; }
        public string ImagePath { get; set; }

        public ICollection<Course> Courses { get; set; }

        protected University()
        {
            
        }
        public University(Guid id, string name, string shortcut, string place, string imagePath)
        {
            Id = id;
            Name = name;
            Shortcut=shortcut;
            Place = place;
            ImagePath = imagePath;
        }
    }
}