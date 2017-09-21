using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using OcenUczelnie.Infrastructure.Settings;

namespace OcenUczelnie.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration) where T : ISettings, new()
        {
            var section = typeof(T).Name.Replace("Settings", "");
            T settings=new T();
            configuration.GetSection(section).Bind(settings);
            return settings;
        }
    }
}