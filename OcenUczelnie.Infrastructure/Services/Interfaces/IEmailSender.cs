using System.Threading.Tasks;

namespace OcenUczelnie.Infrastructure.Services.Interfaces
{
    public interface IEmailSender: IService
    {
        Task SendEmailAsync(string receiver, string title, string body);
    }
}