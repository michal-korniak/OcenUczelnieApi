using AutoMapper;
using OcenUczelnie.Core.Domain;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }

    }
}