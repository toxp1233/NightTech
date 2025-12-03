using AutoMapper;
using NightTech.Application.Users.Commands.CreateAccount;
using NightTech.Domain.Entities;

namespace NightTech.Application.Users.Mapper;

public class UsersMapper : Profile
{
    public UsersMapper()
    {
        CreateMap<CreateAccountCommand, User>();
    }
}
