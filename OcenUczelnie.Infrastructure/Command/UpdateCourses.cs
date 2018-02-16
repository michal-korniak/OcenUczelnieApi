using System;
using System.Collections.Generic;

namespace OcenUczelnie.Infrastructure.Command {

    public class UpdateCourses {
        public class Course {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Department { get; set; }
        }

        public Guid UniversityId { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}