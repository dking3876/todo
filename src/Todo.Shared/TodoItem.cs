using System;

namespace TodoApi.Shared.Models{
    public class TodoItem{
        public long Id {get;set;}
        public string Name {get;set;}
        public bool IsComplete {get;set;}
    }
}