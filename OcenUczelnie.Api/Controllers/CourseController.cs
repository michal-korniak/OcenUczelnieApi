using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.Command;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Api.Controllers
{
    public class CourseController: BaseApiController
    {
        private readonly ICourseService _courseService;
        private readonly IReviewService _reviewService;

        public CourseController(ICourseService courseService, IReviewService reviewService)
        {
            _courseService = courseService;
            _reviewService = reviewService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var course = await _courseService.Get(id);
            return Json(course);
        }
        [HttpGet("{id}/get_reviews")]
        public async Task<IActionResult> GetReviews(Guid id)
        {
            var course = await _reviewService.GetReviews(id);
            return Json(course);
        }
        [Authorize]
        [HttpPost("{id}/post_review")]
        public async Task<IActionResult> PostReview(Guid id, [FromBody]PostReview postReview)
        {
            await _reviewService.PostReview(GetCurrentUserId(), id, postReview.Rating, postReview.Content);
            return Ok();
        }
    }
}