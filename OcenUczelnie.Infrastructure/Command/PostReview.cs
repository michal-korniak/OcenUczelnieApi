using System;

namespace OcenUczelnie.Infrastructure.Command
{
    public class PostReview
    {
        public int Rating { get; set; }
        public string Content { get; set; }
    }
}