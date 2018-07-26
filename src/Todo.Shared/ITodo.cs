using System;
using System.Collections.Generic;

namespace TodoApi.Shared.Models
{
    public interface ITodo
    {
        List<TodoItem> List();

        List<TodoItem> List(bool isComplete, int limit = 5, int offset = 0);
    }
}