using AutoMapper;
using Tockify.Application.Command.ClientUser;
using Tockify.Application.Command.TaskItem;
using Tockify.Application.Command.ToDo;
using Tockify.Application.DTOs;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;

namespace Tockify.Application.Mappings
{
    public class ClientUserProfile : Profile
    {
        public ClientUserProfile()
        {
            CreateMap<CreateClientUserCommand, ClientUserModel>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src.Profile));

            CreateMap<ClientUserModel, ClientUserDto>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src.Profile.ToString()));
        }
    }
}
