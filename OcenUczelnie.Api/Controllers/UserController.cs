using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OcenUczelnie.Infrastructure.Services;
using OcenUczelnie.Infrastructure.Command;
using OcenUczelnie.Infrastructure.Services.Interfaces;

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
        [HttpGet("browseAll")]
        public async Task<IActionResult> BrowseAll()
        {
            return Json(await _userService.BrowseAllAsync());
        }
        [HttpGet("details")]
        [Authorize]
        public async Task<IActionResult> UserDetails()
        {
            var id = GetCurrentUserId();
            var user = await _userService.GetAsync(id);
            return Json(user);
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
            await _userService.UpdateAsync(id, updateUser.Email, updateUser.Name, updateUser.Password);
            return Ok();
        }

        [HttpGet("change_role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeRole([FromBody]ChangeRole changeRole)
        {
            await _userService.ChangeRoleAsync(changeRole.UserId, changeRole.Role);
            return Ok();
        }
    }
}
