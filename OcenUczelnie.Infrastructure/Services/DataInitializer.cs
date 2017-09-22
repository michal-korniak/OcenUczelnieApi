using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;

namespace OcenUczelnie.Infrastructure.Services
{
    public class DataInitializer: IDataInitializer
    {
        private readonly IUserRepository _userRepository;

        public DataInitializer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task SeedAsync()
        {
            if ((await _userRepository.BrowseAllAsync()).Any())
                return;
            for (int i = 0; i < 5; ++i)
            {
                var user = new User(Guid.NewGuid(), $"test{i}@mail.com", $"test{i}", "secret", "salt", "user");
                await _userRepository.AddAsync(user);
            }

        }

    }
}