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
        public async Task<IActionResult> Get(Guid id)
        {
            var university = await _universityService.Get(id);
            return Json(university);
        }

        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetCourses(Guid id)
        {
            var courses = await _universityService.BrowseCoursesAsync(id);
            return Json(courses);
        }
    }
}
