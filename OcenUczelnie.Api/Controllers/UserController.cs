using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OcenUczelnie.Infrastructure.Services;
using OcenUczelnie.Infrastructure.Command;

namespace OcenUczelnie.Api.Controllers
{
 
    public class UserController : BaseApiController
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
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody]UpdateUser updateUser)
        {
            var id = GetCurrentUserId();
            await _userService.Update(id, updateUser.Email, updateUser.Name, updateUser.Password);
            return Ok();
        }

        [HttpGet("change_role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeRole([FromBody]ChangeRole changeRole)
        {
            await _userService.ChangeRole(changeRole.UserId, changeRole.Role);
            return Ok();
        }
        



    }
}
