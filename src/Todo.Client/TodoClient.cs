using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Shared;
using TodoApi.Shared.Models;

namespace Todo.Client
{
    public class TodoClient : ITodo
    {
        public TodoItem Create(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> Getall()
        {
            //make api call to the webapi and return the list of todos
            throw new NotImplementedException();
        }

        public List<TodoItem> Getall(bool IsComplete, int limit = 5, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public TodoItem Getbyid(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(long id, TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}
