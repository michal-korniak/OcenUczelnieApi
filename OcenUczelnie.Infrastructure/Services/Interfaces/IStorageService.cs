using System;
using System.Threading.Tasks;
using OcenUczelnie.Infrastructure.Services.Interfaces;

public interface IStorageService: IService
{
    Task<string> UploadImageAndReturnUrlAsync(string base64Image, string name);

}