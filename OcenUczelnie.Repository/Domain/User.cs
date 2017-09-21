using System;
using System.Collections.Generic;

namespace OcenUczelnie.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}