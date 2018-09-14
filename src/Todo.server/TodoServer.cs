using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Shared;
using TodoApi.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Todo.server
{
    public class TodoServer : ITodo
    {
        private readonly TodoContext _TodoContext;
        private readonly UserContext _UserContext;

        public TodoServer(TodoContext TContext, UserContext UContext) {
            _TodoContext = TContext;
            _UserContext = UContext;
        }

        public async Task<TodoItemPublic> Create(TodoItemPublic item)
        {
            //@todo use automapper 
            TodoItemPrivate TodoItem = new TodoItemPrivate() { Name = item.Name, IsComplete = item.IsComplete, UserId = item.User.Id };


            await _TodoContext.TodoItems.AddAsync(TodoItem);
            _TodoContext.SaveChanges();
            return item;
        }

        public async Task<List<TodoItemPublic>> Getall()
        {
            //@todo use automapper
            List<TodoItemPrivate> TodoPrivate = await _TodoContext.TodoItems.ToListAsync();
            List<TodoItemPublic> TodoPublic = new List<TodoItemPublic>();

            foreach(TodoItemPrivate TodoItem in TodoPrivate)
            {
                TodoPublic.Add(new TodoItemPublic()
                {
                    Id = TodoItem.Id,
                    Name = TodoItem.Name,
                    IsComplete = TodoItem.IsComplete,
                    User = _UserContext.User.Find(TodoItem.UserId)
                });
            }


            return TodoPublic;
        }

        public async Task<List<TodoItemPublic>> Getall(bool IsComplete, int limit = 5, int offset = 0)
        {
            //@todo use automapper
            List<TodoItemPrivate> TodoPrivate = await _TodoContext.TodoItems.ToListAsync();
            List<TodoItemPublic> TodoPublic = new List<TodoItemPublic>();

            foreach (TodoItemPrivate TodoItem in TodoPrivate)
            {
                TodoPublic.Add(new TodoItemPublic()
                {
                    Id = TodoItem.Id,
                    Name = TodoItem.Name,
                    IsComplete = TodoItem.IsComplete,
                    User = _UserContext.User.Find(TodoItem.UserId)
                });
            }


            return TodoPublic;
        }

        public async Task<TodoItemPublic> Getbyid(int id)
        {
            var TodoPrivate = await _TodoContext.TodoItems.FindAsync(id);

            TodoItemPublic TodoPublic = new TodoItemPublic()
            {
                Id = TodoPrivate.Id,
                Name = TodoPrivate.Name,
                IsComplete = TodoPrivate.IsComplete,
                User = _UserContext.User.Find(TodoPrivate.UserId)
            };
            return TodoPublic;
        }

        public async Task<TodoItemPublic> Update(int id, TodoItemPublic item)
        {
            try
            {
                var todo = _TodoContext.TodoItems.Find(id);
                todo.IsComplete = item.IsComplete;
                todo.Name = item.Name;
                todo.UserId = item.User.Id;
                _TodoContext.TodoItems.Update(todo);
                await _TodoContext.SaveChangesAsync();
  
            }
            catch(ContextMarshalException e)
            {
                System.Console.Write(e);
                // handle errors
            }
            return item;
        }
    }
}
