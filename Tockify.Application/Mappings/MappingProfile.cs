using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Domain.Models;

namespace Tockify.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientUserModel, ClientUserDto>();
            CreateMap<CreateClientUserCommand, ClientUserModel>();

            CreateMap<CardModel, ToDoDto>();
            CreateMap<CreateToDoCommand, CardModel>();

            CreateMap<TaskItemModel, TaskItemDto>();
            CreateMap<CreateTaskItemCommand, TaskItemModel>();
        }
    }
}
