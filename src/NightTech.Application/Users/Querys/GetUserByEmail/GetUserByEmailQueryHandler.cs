using AutoMapper;
using MediatR;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Users.Querys.GetUserByEmail;

public class GetUserByEmailQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserByEmailQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email) ?? throw new NotFoundException(nameof(User), request.Email);
        return mapper.Map<UserDto>(user);
    }
}
