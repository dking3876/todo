using System;
using System.Collections.Generic;
using TodoApi.Shared.Models;
using Microsoft.AspNetCore;
using System.Threading.Tasks;

namespace TodoApi.Shared
{
    public interface ITodo
    {
        Task<List<TodoItem>> Getall();

        Task<List<TodoItem>> Getall(bool IsComplete, int limit = 5, int offset = 0);

        Task<TodoItem> Getbyid(long id);

        Task<TodoItem> Create(TodoItem item);

        Task<TodoItem> Update(long id, TodoItem item);
    }

}