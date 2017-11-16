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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetailsById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Json(user);
        }
        [HttpGet("get_by_email/{email}")]
        public async Task<IActionResult> GetUserDetailsByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            return Json(user);
        }
        [HttpGet("current_user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var id = GetCurrentUserId();
            var user = await _userService.GetByIdAsync(id);
            return Json(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUser createUser)
        {
            await _userService.RegisterAsync(createUser.Email, createUser.Name, createUser.Password, "user");
            var id=_memoryCache.Get<Guid>("registeredUserId");
            var user = await _userService.GetByIdAsync(id);
            return Json(user);
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
            await _userService.UpdateAsync(id, updateUser.Name, updateUser.Password);
            return Ok();
        }

        [HttpPost("generate_token/{id}")]
        public async Task<IActionResult> GenerateToken(Guid id)
        {
            await _userService.GenerateConfirmToken(id);
            return Ok();
        }
        [HttpGet("validate_token/{id}/{token}")]
        public async Task<IActionResult> ValidateToken(Guid id, string token)
        {
            await _userService.ValidateConfirmToken(id, token);
            return Ok();
        }
    }
}
