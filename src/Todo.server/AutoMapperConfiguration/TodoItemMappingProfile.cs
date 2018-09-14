using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TodoApi.Shared.Models;

namespace Todo.server.AutoMapperConfiguration
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            this.CreateMap<TodoItemPrivate, TodoItemPublic>()
                //.ForMember( dest=>dest.User, opt=>opt.MapFrom(src=>src.) )
        }

    }
}
