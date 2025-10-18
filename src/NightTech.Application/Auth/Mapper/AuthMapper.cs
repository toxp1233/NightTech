using AutoMapper;
using NightTech.Application.Auth.Commands.Register;
using NightTech.Application.Auth.Dtos;
using NightTech.Domain.Entities;

namespace NightTech.Application.Auth.Mapper;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<User, TokenDto>()
            .ForMember(dest => dest.AccessToken, opt => opt.Ignore());
        CreateMap<RegisterCommand, User>();
    }
}
