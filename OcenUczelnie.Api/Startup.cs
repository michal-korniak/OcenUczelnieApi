using System;
using System.IO;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OcenUczelnie.Api.Middleware;
using OcenUczelnie.Infrastructure.EF;
using OcenUczelnie.Infrastructure.Extensions;
using OcenUczelnie.Infrastructure.IoC;
using OcenUczelnie.Infrastructure.Mappers;
using OcenUczelnie.Infrastructure.Services.Interfaces;
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
            services.AddCors();

            services.AddMvc();
            services.AddSingleton(AutoMapperConfig.Initialize());
            var connectionString = Configuration.GetSettings<SqlSettings>().ConnectionString;
            services.AddDbContext<OcenUczelnieContext>(opt => opt.UseSqlServer(connectionString, b=>b.MigrationsAssembly("OcenUczelnie.Api")));
            services.AddMemoryCache();
            var jwtSettings = Configuration.GetSettings<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
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
            app.UseCors(options => options.WithOrigins("http://localhost:9000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.UseAuthentication() //without this line, api always returns 401 even if token is valid
                .UseMiddleware(typeof(ExceptionMiddleware))
                .UseMvc();
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());


        }
    }
}
