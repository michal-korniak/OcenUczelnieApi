using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services
{
    public interface IUserService: IService
    {
        Task<UserDto> Get(Guid id);
        Task<IEnumerable<UserDto>> BrowseAll();
        Task RegisterAsync(string email, string name, string password, string role);
        Task LoginAsync(string email, string password);
        Task Update(Guid id,string email, string name, string password);
        Task ChangeRole(Guid id, string role);
    }
}