﻿using System;
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

        public TodoServer(TodoContext contex) {
            _context = contex;
        }

        public Task<TodoItem> Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return Task.Run(()=>item);
        }

        public Task<List<TodoItem>> Getall()
        {
            return Task.Run(() => _context.TodoItems.ToList());
        }

        public Task<List<TodoItem>> Getall(bool IsComplete, int limit = 5, int offset = 0)
        {
            return Task.Run(()=>_context.TodoItems.ToList());
        }

        public Task<TodoItem> Getbyid(long id)
        {
            var item = _context.TodoItems.Find(id);
            return Task.Run(()=>item);
        }

        public Task<TodoItem> Update(long id, TodoItem item)
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
                System.Console.Write(e);
                // handle errors
            }
            return Task.Run(()=>item);
        }
    }
}
