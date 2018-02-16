using System;
using System.Collections.Generic;

namespace OcenUczelnie.Core.Domain
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }

        public University University { get; set; }
        public ICollection<Review> Reviews { get; set; }

        protected Course(){}

        public Course(Guid id,string name, string department)
        {
            Id=id;
            Name=name;
            Department=department;
        }


    }
}