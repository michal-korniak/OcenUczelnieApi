﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IReviewService: IService
    {
        Task PostReview(Guid userId, Guid courseId, int rating, string content);
        Task<IEnumerable<ReviewDto>> GetReviews(Guid courseId);
    }
}