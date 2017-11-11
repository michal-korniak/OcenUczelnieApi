
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OcenUczelnie.Infrastructure.Services.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OcenUczelnie.Api.Controllers
{
    public class ReviewController: BaseApiController
    {
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        private readonly IReviewService _reviewService;

        [Authorize]
        [HttpPost("{reviewId}/approve")]
        public async Task<IActionResult> ApproveReview(Guid reviewId)
        {
            await _reviewService.ApproveReview(GetCurrentUserId(),reviewId);
            return Ok();
        }
        [Authorize]
        [HttpPost("{reviewId}/disapprove")]
        public async Task<IActionResult> DisapproveReview(Guid reviewId)
        {
            await _reviewService.DisapproveReview(GetCurrentUserId(), reviewId);
            return Ok();
        }
    }
}
