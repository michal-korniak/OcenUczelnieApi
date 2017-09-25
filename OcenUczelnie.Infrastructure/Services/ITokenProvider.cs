using System;
using System.IdentityModel.Tokens.Jwt;

namespace OcenUczelnie.Infrastructure.Services
{
    public interface ITokenProvider: IService
    {
        string CreateToken(Guid userId, string role);
    }
}