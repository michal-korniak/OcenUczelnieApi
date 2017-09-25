using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OcenUczelnie.Infrastructure.Services;
using OcenUczelnie.Infrastructure.Command;

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
        [Authorize]
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUser createUser)
        {
            await _userService.RegisterAsync(createUser.Email, createUser.Name, createUser.Password, "user");
            var id=_memoryCache.Get<Guid>("registeredUserId");
            return Created($"user/{id}", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUser loginUser)
        {
            await _userService.LoginAsync(loginUser.Email, loginUser.Password);
            var token = _memoryCache.Get("generatedToken");
            return Json(token);
        }

    }
}
