using System;

namespace OcenUczelnie.Infrastructure.Command
{
    public class CreateUser
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
