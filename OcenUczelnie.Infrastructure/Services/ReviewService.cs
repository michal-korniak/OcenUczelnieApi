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

        public ReviewService(IReviewRepository reviewRepository, ICourseRepository courseRepository,IUserRepository userRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task PostReview(Guid userId, Guid courseId, int rating, string content)
        {
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
            var priorUserMark = _reviewRepository.GetUserMarkToReview(userId, reviewId);
            if (priorUserMark == 1)
                throw new Exception("User already approved this review.");
            else if (priorUserMark == -1)
                await _reviewRepository.RemoveUserReviewDisapproved(userId, reviewId);
            await _reviewRepository.AddUserReviewApproved(userId, reviewId);
        }

        public async Task DisapproveReview(Guid userId, Guid reviewId)
        {
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

    }
}