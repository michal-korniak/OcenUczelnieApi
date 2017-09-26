using System.Threading.Tasks;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IDataInitializer: IService
    {
        Task SeedAsync();
    }
}