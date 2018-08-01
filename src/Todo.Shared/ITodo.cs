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

        List<TodoItem> Getall(bool IsComplete, int limit = 5, int offset = 0);

        TodoItem Getbyid(long id);

        TodoItem Create(TodoItem item);

        TodoItem Update(long id, TodoItem item);
    }

}