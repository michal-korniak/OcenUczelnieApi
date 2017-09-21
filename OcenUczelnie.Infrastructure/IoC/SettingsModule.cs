﻿using Autofac;
using Microsoft.Extensions.Configuration;
using OcenUczelnie.Infrastructure.Extensions;
using OcenUczelnie.Infrastructure.Settings;
using Module = Autofac.Module;


namespace OcenUczelnie.Infrastructure.IoC
{
    public class SettingsModule: Module
    {
        private readonly IConfiguration _config;

        public SettingsModule(IConfiguration config)
        {
            _config = config;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_config.GetSettings<SqlSettings>())
                .AsSelf()
                .SingleInstance();
        }
    }
}