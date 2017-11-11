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

        public ICollection<Review> Reviews { get; set; }
        public ICollection<ReviewUserApproved> ReviewUserApproved { get; set; }
        public ICollection<ReviewUserDisapproved> ReviewUserDisapproved { get; set; }

        public User(Guid id, string email, string name, string password, string salt, string role)
        {
            Id = id;
            Email = email;
            Name = name;
            Password = password;
            Salt = salt;
            Role = role;
        }
        protected User()
        {
            
        }
    }
}