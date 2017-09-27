using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Core.Repositories;
using OcenUczelnie.Infrastructure.DTO;

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
                    cfg.CreateMap<University, UniversityDto>();
                    cfg.CreateMap<Review, ReviewDto>();
                })
                .CreateMapper();
    }
}