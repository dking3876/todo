using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Shared;
using TodoApi.Shared.Models;

namespace Todo.server
{
    public class TodoServer : ITodo
    {
        private readonly TodoContext _context;

        public TodoServer(TodoContext contex) { }

        public TodoItem Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public Task<List<TodoItem>> Getall()
        {
            //List<TodoItem> items = await _context.TodoItems.ToList();
            //return await _context.TodoItems.ToList();
            return Task.Run(() => _context.TodoItems.ToList());
        }

        public List<TodoItem> Getall(bool IsComplete, int limit = 5, int offset = 0)
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem Getbyid(long id)
        {
            var item = _context.TodoItems.Find(id);
            return item;
        }

        public TodoItem Update(long id, TodoItem item)
        {
            try
            {
                var todo = _context.TodoItems.Find(id);
                todo.IsComplete = item.IsComplete;
                todo.Name = item.Name;
                _context.TodoItems.Update(todo);
                _context.SaveChanges();
  
            }
            catch(ContextMarshalException e)
            {
                // handle errors
            }
            return item;
        }
    }
}
