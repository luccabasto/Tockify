using AutoMapper;
using Tockify.Application.DTOs;
using Tockify.Domain.Models;

namespace Tockify.Application.Mappings
{
    public class ClientUserProfile : Profile
    {
        public ClientUserProfile()
        {
            CreateMap<ClientUserModel, ClientUserDto>()
                .ForMember(dest => dest.IncompleteToDosCount,
                           opt => opt.MapFrom(src => src.IncompleteToDosCount))
                .ForMember(dest => dest.ToDos, opt => opt.Ignore());
        }
    }
}
