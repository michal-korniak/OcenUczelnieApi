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
        private readonly IReviewRepository _reviewRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, ICourseRepository courseRepository, IUserRepository userRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task PostReview(Guid userId, Guid courseId, int rating, string content)
        {
            if(rating<1 || rating>5)
            {
                throw new Exception("Rating should be in range from 1 to 5");
            }
            if (content == null || content.Length < 20)
                throw new Exception("Content of review doesn't exist or has less than 20 characters.");
            var user = await _userRepository.GetByIdAsync(userId);
            var course = await _courseRepository.GetByIdAsync(courseId);
            var review = new Review(new Guid(), user, course, rating, content);
            await _reviewRepository.AddAsync(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviews(Guid courseId)
        {
            var reviews = await _reviewRepository.GetReviewsForCourse(courseId);
            return _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(reviews);
        }

        public async Task ApproveReview(Guid userId, Guid reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review.User.Id == userId)
                throw new Exception("User cannot approve his own review.");
            var priorUserMark = _reviewRepository.GetUserMarkToReview(userId, reviewId);
            if (priorUserMark == 1)
                throw new Exception("User already approved this review.");
            else if (priorUserMark == -1)
                await _reviewRepository.RemoveUserReviewDisapproved(userId, reviewId);
            await _reviewRepository.AddUserReviewApproved(userId, reviewId);
        }

        public async Task DisapproveReview(Guid userId, Guid reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review.User.Id == userId)
                throw new Exception("User cannot disapprove his own review.");
            var priorUserMark = _reviewRepository.GetUserMarkToReview(userId, reviewId);
            if (priorUserMark == -1)
                throw new Exception("User already disapproved this review.");
            else if (priorUserMark == 1)
                await _reviewRepository.RemoveUserReviewApproved(userId, reviewId);
            await _reviewRepository.AddUserReviewDisapproved(userId, reviewId);
        }

        public int GetReviewPoints(Guid reviewId)
        {
            return _reviewRepository.GetReviewPoints(reviewId);
        }

        public int GetUserMarkToReview(Guid userId, Guid reviewId)
        {
            return _reviewRepository.GetUserMarkToReview(userId, reviewId);
        }

        public async Task DeleteApproveFromReview(Guid userId, Guid reviewId)
        {
            await _reviewRepository.RemoveUserReviewApproved(userId, reviewId);
        }
        public async Task DeleteDisapproveFromReview(Guid userId, Guid reviewId)
        {
            await _reviewRepository.RemoveUserReviewDisapproved(userId, reviewId);
        }

        public async Task RemoveReview(Guid userId, Guid reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if(userId!=review.User.Id)
            {
                throw new Exception("This user is not an author of this review.");
            }
            await _reviewRepository.RemoveAsync(reviewId);
        }
    }
}