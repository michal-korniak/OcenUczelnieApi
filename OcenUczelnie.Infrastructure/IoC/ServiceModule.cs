using System.IO;
using Autofac;
using OcenUczelnie.Infrastructure.Services;
using OcenUczelnie.Infrastructure.Services.Interfaces;
using SimpleCrypto;

namespace OcenUczelnie.Infrastructure.IoC
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var asm = typeof(ServiceModule).Assembly;

            builder.RegisterAssemblyTypes(asm)
                .AssignableTo<IService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<PBKDF2>()
                .As<ICryptoService>();
        }
    }
}