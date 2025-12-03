using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Users.Querys.GetUserByUserName;

public class GetUserByUserNameQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserByUserNameQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByNameAsync(request.UserName) ?? throw new NotFoundException(nameof(User), request.UserName);
        return mapper.Map<UserDto>(user);
    }
}
