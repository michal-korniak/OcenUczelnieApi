using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.IoC;
using OcenUczelnie.Infrastructure.Services;
using OcenUczelnie.Infrastructure.Settings;

namespace OcenUczelnie.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IUserService _userService;


        // GET api/values
        public ValuesController(IUserService userService)
        {
            _userService = userService;
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
    }
}
