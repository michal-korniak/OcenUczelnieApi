using System;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface ITokenProvider: IService
    {
        string CreateToken(Guid userId, string role);
    }
}