using System.Collections.Generic;
using Autofac;
using OcenUczelnie.Core.Repositories;

namespace OcenUczelnie.Infrastructure.IoC
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var asm = typeof(RepositoryModule).Assembly;
            builder.RegisterAssemblyTypes(asm)
                .AssignableTo<IRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}