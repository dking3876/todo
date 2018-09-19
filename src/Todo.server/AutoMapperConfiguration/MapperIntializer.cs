using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.server.AutoMapperConfiguration
{
    public class MapperIntializer
    {
        public static void Initialize()
        {
            Mapper.Initialize( config =>
            {
                config.AddProfile<TodoItemMappingProfile>();
            });
        }
    }
}
