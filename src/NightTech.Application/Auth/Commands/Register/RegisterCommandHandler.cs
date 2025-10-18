using AutoMapper;
using MediatR;
using NightTech.Application.Auth.Dtos;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;
namespace NightTech.Application.Auth.Commands.Register;

public class RegisterCommandHandler
    (
    IUserRepository userRepository, 
    IUnitOfWork unitOfWork, 
    IMapper mapper,
    IPasswordHasherBcrypt passwordHasher, 
    IJwtService jwtService,
    ICartRepository cartRepository
    ) : IRequestHandler<RegisterCommand, TokenDto>
{
    public async Task<TokenDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUserName = await userRepository.GetByNameAsync(request.UserName);
        var existingUserEmail = await userRepository.GetByEmailAsync(request.Email);


        if (existingUserName != null)
        {
            throw new AlreadyExistsException(nameof(User), "Username");
        } else if (existingUserEmail != null)
        {
            throw new AlreadyExistsException(nameof(User), "Email");
        }

        var newUser = mapper.Map<User>(request);
        newUser.PasswordHash = passwordHasher.Hash(request.Password);
        var createdUser = await userRepository.CreateAsync(newUser);
        var cart = await cartRepository.CreateAsync(new Cart
        {
            UserId = createdUser.Id,
            TotalPrice = 0,
            ItemCount = 0
        });

        await unitOfWork.SaveChangesAsync();

        var token = jwtService.GenerateToken(createdUser);

        createdUser.RefreshToken = jwtService.GenerateRefreshToken();
        createdUser.RefreshTokenExpireDate = jwtService.GenerateRefreshTokenExpiry();
        createdUser.CartId = cart.Id;
        await unitOfWork.SaveChangesAsync();

        return new TokenDto
        {
            AccessToken = token,
            RefreshToken = createdUser.RefreshToken
        };

    }
}
