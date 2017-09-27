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
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task PostReview(Guid userId, Guid courseId, int rating, string content)
        {
            var review = new Review(new Guid(), userId, courseId, rating, content);
            await _reviewRepository.AddAsync(review);
        }

        public async Task<IEnumerable<ReviewDto>> GetReviews(Guid courseId)
        {
            var reviews = await _reviewRepository.GetReviewsForCourse(courseId);
            return _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(reviews);
        }
    }
}