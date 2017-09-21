using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.IoC;
using OcenUczelnie.Infrastructure.Settings;

namespace OcenUczelnie.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly IConfiguration _config;
        private readonly SqlSettings _sqlSettings;
        private readonly IUserRepository _userRepository;

        // GET api/values
        public ValuesController(IConfiguration config, SqlSettings sqlSettings, IUserRepository userRepository)
        {
            _config = config;
            _sqlSettings = sqlSettings;
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(_userRepository.BrowseAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
