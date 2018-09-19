using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Shared;
using TodoApi.Shared.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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
            TodoItemPrivate TodoItem = new TodoItemPrivate() { Name = item.Name, IsComplete = item.IsComplete, UserId = item.User.Id };


            await _TodoContext.TodoItems.AddAsync(TodoItem);
            _TodoContext.SaveChanges();
            return item;
        }

        public async Task<List<TodoItemPublic>> Getall()
        {
            List<TodoItemPrivate> TodoPrivate = await _TodoContext.TodoItems.ToListAsync();

            List<TodoItemPublic> TodoPublic = Mapper.Map<List<TodoItemPublic>>(TodoPrivate);

            TodoPublic.ForEach(TodoItem =>{
                   TodoItem.User = _UserContext.User.Find(TodoItem.User.Id);
             });

            return TodoPublic;
        }

        public async Task<List<TodoItemPublic>> Getall(bool IsComplete, int limit = 5, int offset = 0)
        {
 
            List<TodoItemPrivate> TodoPrivate = await _TodoContext.TodoItems.Where( _item=>_item.IsComplete).Take(limit).ToListAsync();

            List<TodoItemPublic> TodoPublic = Mapper.Map<List<TodoItemPublic>>(TodoPrivate);

            TodoPublic.ForEach(TodoItem => {
                TodoItem.User = _UserContext.User.Find(TodoItem.User.Id);
            });

            return TodoPublic;
        }

        public async Task<TodoItemPublic> Getbyid(int id)
        {
            var TodoPrivate = await _TodoContext.TodoItems.FindAsync(id);

            TodoItemPublic TodoPublic = Mapper.Map<TodoItemPublic>(TodoPrivate);

            TodoPublic.User = _UserContext.User.Find(TodoPublic.User.Id);
            return TodoPublic;
        }

        public async Task<TodoItemPublic> Update(int id, TodoItemPublic item)
        {
            try
            {
                TodoItemPrivate TodoPrivate = Mapper.Map<TodoItemPrivate>(item);

                _TodoContext.TodoItems.Update(TodoPrivate);
                await _TodoContext.SaveChangesAsync();
                return Getbyid(TodoPrivate.Id).Result;

            }
            catch(ContextMarshalException e)
            {
                System.Console.Write(e);
                // handle errors
            }
            return null;
        }
    }
}
