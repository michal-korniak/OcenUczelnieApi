using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.DTO;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IUniversityService: IService
    {
        Task AddAsync(string name, string place, string imagePath);
        Task<UniversityDto> GetAsync(Guid id);
        Task<UniversityDetailsDto> GetDetailsAsync(Guid id);
        Task<IEnumerable<UniversityDto>> BrowseAllAsync();
    }
}