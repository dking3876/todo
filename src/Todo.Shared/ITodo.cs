using System;
using System.Collections.Generic;
using TodoApi.Shared.Models;
using Microsoft.AspNetCore;
using System.Threading.Tasks;

namespace TodoApi.Shared
{
    public interface ITodo
    {
        Task<List<TodoItemPublic>> Getall();

        Task<List<TodoItemPublic>> Getall(bool IsComplete, int limit = 5, int offset = 0);

        Task<TodoItemPublic> Getbyid(int id);

        Task<TodoItemPublic> Create(TodoItemPublic item);

        Task<TodoItemPublic> Update(int id, TodoItemPublic item);
    }

}