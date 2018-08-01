using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Shared.Models;

namespace TodoApi.Shared.Models{
    public class TodoContext: DbContext{

            public TodoContext(DbContextOptions<TodoContext> options) : base (options){

            }
            public DbSet<TodoItem> TodoItems {get;set;}
    }
}