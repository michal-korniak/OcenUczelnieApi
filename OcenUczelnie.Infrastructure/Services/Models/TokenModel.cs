using System;

namespace OcenUczelnie.Infrastructure.Services.Models
{
    public class TokenModel
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public long Expires { get; set; }


    }
}
