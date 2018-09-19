using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TodoApi.Shared;
using TodoApi.Shared.Models;
using NodaTime;
namespace Todo.server.AutoMapperConfiguration
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            this.CreateMap<TodoItemPrivate, TodoItemPublic>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserId))
                //this may not be the best way to do this... probably should create type conversion
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => Instant.FromUnixTimeSeconds(src.CreatedDate)));

            this.CreateMap<int, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            
            this.CreateMap<TodoItemPublic, TodoItemPrivate>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

        }

    }
}
