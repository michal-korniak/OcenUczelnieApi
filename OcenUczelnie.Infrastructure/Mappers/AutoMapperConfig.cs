using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;
using System;
using System.Linq;

namespace OcenUczelnie.Infrastructure.Mappers
{

    public class AutoMapperConfig
    {
        private readonly IUniversityRepository _universityRepository;

        public AutoMapperConfig(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDto>();
                    cfg.CreateMap<Course, CourseDto>();
                    cfg.CreateMap<Course, CourseDetailsDto>()
                        .AfterMap((c, dto) => dto.CountRating = c.Reviews.Count)
                        .AfterMap((c, dto) => dto.AvgRating = c.Reviews.Count>0?Math.Round(c.Reviews.AsQueryable().Average(r => r.Rating),2):0);
                    cfg.CreateMap<University, UniversityDto>();
                    cfg.CreateMap<University, UniversityDetailsDto>();
                    cfg.CreateMap<Review, ReviewDto>()
                        .AfterMap((r, dto) => dto.Points = r.ReviewUserApproved.Count - r.ReviewUserDisapproved.Count);
                })
                .CreateMapper();

    }
}