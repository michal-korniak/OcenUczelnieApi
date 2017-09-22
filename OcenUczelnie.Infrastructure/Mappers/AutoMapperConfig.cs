using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Mappers
{

    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDto>();
                })
                .CreateMapper();
    }
}