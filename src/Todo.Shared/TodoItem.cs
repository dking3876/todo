using System;

namespace TodoApi.Shared.Models{
    public class TodoItem : TodoBase{
        public long Id {get;set;}
        public bool IsComplete {get;set;}
    }
}