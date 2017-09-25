using System;

namespace OcenUczelnie.Infrastructure.Command
{
    public class ChangeRole
    {
        public Guid  UserId { get; set; }
        public string Role { get; set; }
    }
}