using System.Threading.Tasks;

namespace OcenUczelnie.Infrastructure.Services
{
    public interface IDataInitializer: IService
    {
        Task SeedAsync();
    }
}