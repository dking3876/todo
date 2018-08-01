using System;
using System.Collections.Generic;
using Todo.Client;
using TodoApi.Shared.Models;

namespace Todo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
           TodoClient TodoClient = new TodoClient();

            System.Console.WriteLine("Hello World!");

            List<TodoItem> todos = TodoClient.Getall();

            foreach( TodoItem todo in todos)
            {
                System.Console.WriteLine(todo.ToString());
            }

        }
    }
}
