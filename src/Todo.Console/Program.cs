using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Client;
using TodoApi.Shared.Models;

namespace Todo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
           TodoClient TodoClient = new TodoClient();
            try
            {
                System.Console.WriteLine("Hello World!");

                //TodoItem todo = new TodoItem();
                //todo.Name = "a fresh one";
                //todo.IsComplete = false;
                //var item = TodoClient.Create(todo).Result;
                //System.Console.WriteLine(item.Name);
                //item.Wait();

                List<TodoItem> todos = TodoClient.Getall().Result;


                System.Console.WriteLine(" after get all new attempt");
                foreach (TodoItem todo in todos)
                {
                    System.Console.WriteLine(todo.Name);
                }
            }
            catch (Exception e){
                System.Console.WriteLine(e.ToString());
            }
        }
        private T DeserializeData<T>(string data)
        {

            var jsonSerialiserSettings = new JsonSerializerSettings();

            var deserialisedObject = JsonConvert.DeserializeObject<T>(data, jsonSerialiserSettings);
            return deserialisedObject;
        }
    }
}
