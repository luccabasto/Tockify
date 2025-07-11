﻿using AutoMapper;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;
using Tockify.Domain.Models;

namespace Tockify.Application.Mappings
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile() 
        {
            CreateMap<CreateToDoCommand, ToDoModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());


            CreateMap<UpdateToDoCommand, ToDoModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ToDoModel, ToDoDto>()
                .ForMember(dest => dest.TotalTasksCount, opt => opt.Ignore())
                .ForMember(dest => dest.PendingTasksCount, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedTasksCount, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedTasks, opt => opt.Ignore());

            CreateMap<ToDoModel, ToDoDto>();
        }
    }
}
