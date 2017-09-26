using System;

namespace OcenUczelnie.Core.Domain
{
    public class University
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }

        protected University()
        {
            
        }
        public University(Guid id, string name, string place)
        {
            Id = id;
            Name = name;
            Place = place;
        }
    }
}