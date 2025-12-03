using AutoMapper;
using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Users.Commands.CreateAccount;

public class CreateAccountCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IPasswordHasherBcrypt passwordHasher,
    ICartRepository cartRepository
    ) : IRequestHandler<CreateAccountCommand, string>
{
    public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var existingUserName = await userRepository.GetByNameAsync(request.UserName);
        var existingUserEmail = await userRepository.GetByEmailAsync(request.Email);


        if (existingUserName != null)
        {
            throw new AlreadyExistsException(nameof(User), "Username");
        }
        else if (existingUserEmail != null)
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

        createdUser.CartId = cart.Id;
        await unitOfWork.SaveChangesAsync();

        return "User account created successfully.";
    }
}


