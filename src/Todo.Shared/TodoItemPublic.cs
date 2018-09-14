using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Shared.Models;

namespace TodoApi.Shared.Models
{
    public class TodoItemPublic:TodoBase
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public User User { get; set; }
    }
}
