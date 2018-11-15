using System;
using System.Collections.Generic;
using System.Text;


namespace TodoApi.Shared
{
    public class CreateTodoItem
    {
        public string Name { get; set; }

        public User User { get; set; }
    }
}
