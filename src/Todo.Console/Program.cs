using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Todo.Client;
using TodoApi.Shared.Models;

namespace Todo.Console
{
    class Program
    {
        static IConfiguration _configuration;
        static void Main(string[] args)
        {
           Program._configuration = setConfiguration();
           TodoClient TodoClient = new TodoClient(Program._configuration);
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
        private static IConfiguration setConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                builder.AddUserSecrets("test-console-app");


            builder.AddEnvironmentVariables();
            return builder.Build();
        }
        private T DeserializeData<T>(string data)
        {

            var jsonSerialiserSettings = new JsonSerializerSettings();

            var deserialisedObject = JsonConvert.DeserializeObject<T>(data, jsonSerialiserSettings);
            return deserialisedObject;
        }
    }
}
