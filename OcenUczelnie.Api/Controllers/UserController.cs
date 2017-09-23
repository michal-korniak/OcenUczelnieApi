using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.IoC;
using OcenUczelnie.Infrastructure.Services;
using OcenUczelnie.Infrastructure.Command;
using SimpleCrypto;

namespace OcenUczelnie.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;

        public UserController(IUserService userService, IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _userService.BrowseAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Json(await _userService.Get(id));
        }

        public async Task<IActionResult> Post([FromBody]CreateUser createUser)
        {
            await _userService.RegisterAsync(createUser.Email, createUser.Name, createUser.Password, "user");
            var id=_memoryCache.Get<Guid>("registeredUserId");
            return Created($"user/{id}", null);
        }
    }
}
