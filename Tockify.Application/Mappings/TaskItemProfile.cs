using AutoMapper;
using Tockify.Application.Command.TaskItem;
using Tockify.Application.DTOs;
using Tockify.Domain.Models;


namespace Tockify.Application.Mappings
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile() {

            CreateMap<CreateTaskItemCommand, TaskItemModel>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                 .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
                 .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<UpdateTaskItemCommand, TaskItemModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TaskItemModel, TaskItemDto>();
            CreateMap<TaskItemModel, TaskItemSummaryDto>();
        }
    }
}
