using System;
using OcenUczelnie.Infrastructure.Services.Models;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface ITokenProvider: IService
    {
        TokenModel CreateToken(Guid userId, string role);
    }
}