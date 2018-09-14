using System;

namespace TodoApi.Shared.Models{
    public class TodoItemPrivate : TodoBase{
        public int Id {get;set;}
        public bool IsComplete {get;set;}
        public int UserId { get; set; }
    }
}