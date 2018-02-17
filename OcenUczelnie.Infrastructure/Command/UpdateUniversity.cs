using System;

namespace OcenUczelnie.Infrastructure.Command
{    public class UpdateUniversity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public string Place { get; set; }
        public string Base64Image { get; set; }
    }
}