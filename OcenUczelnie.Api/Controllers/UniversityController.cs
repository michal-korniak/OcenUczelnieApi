using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Api.Controllers
{
    public class UniversityController : BaseApiController
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var universities = await _universityService.BrowseAllAsync();
            return Json(universities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(Guid id)
        {
            var courses = await _universityService.GetDetailsAsync(id);
            return Json(courses);
        }
    }
}
