using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using TodoApi.Shared.Models;
using TodoApi.Shared;
using Todo.server;
using Todo.server.AutoMapperConfiguration;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Todo.Web
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        public Startup(IHostingEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
 
                builder.AddUserSecrets("1c9cb645-4d07-4eb5-ad9c-2491fb9262ba");
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
    
            services.AddDbContext<TodoContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<UserContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

    

            //services.AddScoped<ITodo, TodoServer>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<TodoServer>().As<ITodo>();

            this.ApplicationContainer = builder.Build();

            MapperIntializer.Initialize();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterType<TodoServer>().As<ITodo>();

        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
