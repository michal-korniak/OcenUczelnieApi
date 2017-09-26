using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Infrastructure.Services
{
    public class DataInitializer: IDataInitializer
    {
        private readonly IUserService _userService;


        public DataInitializer(IUserService userService)
        {
            _userService = userService;
        }
        public async Task SeedAsync()
        {
            if ((await _userService.BrowseAllAsync()).Any())
                return;
            for (int i = 1; i <= 5; ++i)
            {
                await _userService.RegisterAsync($"user{i}@test.com", $"user{i}", "secret","user");
            }
            for (int i = 1; i <= 2; ++i)
            {
                await _userService.RegisterAsync($"admin{i}@test.com", $"admin{i}", "secret", "user");
            }

        }

    }
}