using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TodoApi.Shared.Models;
using Autofac;
using TodoApi.Shared;
using Autofac.Extensions.DependencyInjection;
using Todo.Client;
using Todo.Settings;
using Todo.Autofac;
using Brafton.Common;

namespace Todo.Console
{
    class Program
    {
        private static IConfigurationRoot configuration;

        private static string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        //static IConfiguration _configuration;
        static void Main(string[] args)
        {
            //Program._configuration = setConfiguration();
            //TodoClient TodoClient = new TodoClient(Program._configuration);
            var diContainer = ContainerBuilder();
            var TodoClient = diContainer.Resolve<ITodo>();
            try
            {
                
                System.Console.WriteLine("Hello World!");

                TodoItemPublic todo = new TodoItemPublic();
                todo.Name = "a fresh one";
                todo.IsComplete = false;
                todo.User = new TodoApi.Shared.User
                {
                    Id = 4,
                    FirstName = "",
                    Fullname = "",
                    LastName = ""
                };
                var item = TodoClient.Create(todo).Result;
                System.Console.WriteLine(item.Name);


                List<TodoItemPublic> todos = TodoClient.Getall().Result;


                System.Console.WriteLine("after get all new attempt");
                foreach (TodoItemPublic _todo in todos)
                {
                    System.Console.WriteLine(_todo.Name);
                }
                System.Console.WriteLine("Sleeping");
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e){
                System.Console.WriteLine(e.ToString());
            }
        }
        private static IContainer ContainerBuilder()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                configurationBuilder.AddUserSecrets("test-console-app");

            configuration = configurationBuilder.Build();

            var services = new ServiceCollection();
            services.AddOptions();
      
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //builder.AddEnvironmentVariables();
            //return builder.Build();

            services.Configure<Todo.Settings.TodoSettings>(configuration.GetSection("Brafton:Todos"));
            services.Configure<Brafton.Settings.SecuritySettings>(configuration.GetSection("Brafton:Security"));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            var callerVersions = CallerVersions.GetVersions(typeof(TodoClient), typeof(TodoSettings));
            containerBuilder.RegisterModule(new TodoApiAutofacModule(callerVersions));
            var container = containerBuilder.Build();
            return container;
        }
        private T DeserializeData<T>(string data)
        {

            var jsonSerialiserSettings = new JsonSerializerSettings();

            var deserialisedObject = JsonConvert.DeserializeObject<T>(data, jsonSerialiserSettings);
            return deserialisedObject;
        }
    }
}
