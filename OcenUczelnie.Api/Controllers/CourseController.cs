using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.Command;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Api.Controllers
{
    public class CourseController : BaseApiController
    {
        private readonly ICourseService _courseService;
        private readonly IReviewService _reviewService;

        public CourseController(ICourseService courseService, IReviewService reviewService)
        {
            _courseService = courseService; 
            _reviewService = reviewService;
        }
        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviews(Guid id)
        {
            var course = await _reviewService.GetReviewsAsync(id);
            return Json(course);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(Guid id)
        {
            var course = await _courseService.GetDetailsAsync(id);
            return Json(course);
        }

    }
}