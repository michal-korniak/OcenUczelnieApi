using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IUniversityService: IService
    {
        Task AddAsync(string name, string place, string imagePath);
        Task<UniversityDto> Get(Guid id);
        Task<IEnumerable<UniversityDto>> BrowseAllAsync();
    }
}