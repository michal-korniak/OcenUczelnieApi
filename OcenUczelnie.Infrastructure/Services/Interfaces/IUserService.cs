using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IUserService: IService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAllAsync();
        Task RegisterAsync(string email, string name, string password, string role);
        Task LoginAsync(string email, string password);
        Task UpdateAsync(Guid id, string name, string password);
        Task ChangeRoleAsync(Guid id, string role);
        Task GenerateConfirmToken(Guid userId);
        Task ValidateConfirmToken(Guid userId, string token);
    }
}