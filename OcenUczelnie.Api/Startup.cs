using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OcenUczelnie.Infrastructure.EF;
using OcenUczelnie.Infrastructure.Extensions;
using OcenUczelnie.Infrastructure.IoC;
using OcenUczelnie.Infrastructure.Mappers;
using OcenUczelnie.Infrastructure.Services;
using OcenUczelnie.Infrastructure.Settings;

namespace OcenUczelnie.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
            services.AddSingleton(AutoMapperConfig.Initialize());
            var connectionString = Configuration.GetSettings<SqlSettings>().ConnectionString;
            services.AddDbContext<OcenUczelnieContext>(opt => opt.UseSqlServer(connectionString));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new SettingsModule(Configuration));
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (Configuration.GetSettings<GeneralSettings>().SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();

            }
            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());


        }
    }
}
