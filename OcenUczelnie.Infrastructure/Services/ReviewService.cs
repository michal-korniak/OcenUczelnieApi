using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;
using OcenUczelnie.Infrastructure.Services.Interfaces;

namespace OcenUczelnie.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewOpinionRepository _reviewOpinionRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper,
            IReviewOpinionRepository reviewOpinionRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _reviewOpinionRepository = reviewOpinionRepository;
        }

        public async Task PostReviewAsync(Guid userId, Guid courseId, int rating, string content)
        {
            if (rating < 1 || rating > 5)
                throw new Exception("Rating should be in range from 1 to 5");
            if (content == null || content.Length < 20)
                throw new Exception("Content of review doesn't exist or has less than 20 characters.");
            var review = new Review(new Guid(), userId, courseId, rating, content);
            await _reviewRepository.AddAsync(review);
        }

        public async Task RemoveReviewAsync(Guid userId, Guid reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId,true);
            if (userId != review.User.Id)
                throw new Exception("This user is not an author of this review.");
            await _reviewOpinionRepository.RemoveAllOpinions(reviewId);
            await _reviewRepository.RemoveAsync(reviewId);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsAsync(Guid courseId)
        {
            var reviews = await _reviewRepository.GetReviewsForCourseAsync(courseId,true);
            return _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(reviews);
        }

        public async Task ApproveReviewAsync(Guid userId, Guid reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review.User.Id == userId)
                throw new Exception("User cannot approve his own review.");
            var priorUserMark = _reviewOpinionRepository.GetUserOpinion(userId, reviewId);
            if (priorUserMark == 1)
                throw new Exception("User already approved this review.");
            if (priorUserMark == -1)
                await _reviewOpinionRepository.RemoveDisapproveAsync(userId, reviewId);
            await _reviewOpinionRepository.AddApproveAsync(userId, reviewId);
        }

        public async Task DeleteApproveAsync(Guid userId, Guid reviewId)
        {
            await _reviewOpinionRepository.RemoveApproveAsync(userId, reviewId);
        }

        public async Task DisapproveReviewAsync(Guid userId, Guid reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review.User.Id == userId)
                throw new Exception("User cannot disapprove his own review.");
            var priorUserMark = _reviewOpinionRepository.GetUserOpinion(userId, reviewId);
            if (priorUserMark == -1)
                throw new Exception("User already disapproved this review.");
            if (priorUserMark == 1)
                await _reviewOpinionRepository.RemoveApproveAsync(userId, reviewId);
            await _reviewOpinionRepository.AddDisapproveAsync(userId, reviewId);
        }

        public async Task DeleteDisapproveAsync(Guid userId, Guid reviewId)
        {
            await _reviewOpinionRepository.RemoveDisapproveAsync(userId, reviewId);
        }

        public int GetUserOpinionToReview(Guid userId, Guid reviewId)
        {
            return _reviewOpinionRepository.GetUserOpinion(userId, reviewId);
        }
    }
}