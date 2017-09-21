using System;

namespace OcenUczelnie.Core.Domain
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid UniversityId { get; set; }
        public string Course { get; set; }
        public int Mark { get; set; }
        public string Content { get; set; }

    }
}