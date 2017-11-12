
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
        [HttpDelete("{reviewId}/delete_approve")]
        public async Task<IActionResult> DeleteApproveFromReview(Guid reviewId)
        {
            await _reviewService.DeleteApproveFromReview(GetCurrentUserId(), reviewId);
            return Ok();
        }
        [Authorize]
        [HttpPost("{reviewId}/disapprove")]
        public async Task<IActionResult> DisapproveReview(Guid reviewId)
        {
            await _reviewService.DisapproveReview(GetCurrentUserId(), reviewId);
            return Ok();
        }
        [Authorize]
        [HttpDelete("{reviewId}/delete_disapprove")]
        public async Task<IActionResult> DeleteDisapproveFromReview(Guid reviewId)
        {
            await _reviewService.DeleteDisapproveFromReview(GetCurrentUserId(), reviewId);
            return Ok();
        }
        [Authorize]
        [HttpGet("{reviewId}/mark")]
        public IActionResult GetUserMarkToReview(Guid reviewId)
        {
            return Json(_reviewService.GetUserMarkToReview(GetCurrentUserId(), reviewId));
        }
        [Authorize]
        [HttpDelete("{reviewId}/delete")]
        public async Task<IActionResult> RemoveReview(Guid reviewId)
        {
            await _reviewService.RemoveReview(GetCurrentUserId(), reviewId);
            return Ok();
        }

    }
}
