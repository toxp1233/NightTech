using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Users.Commands.DisableUser;

public class DisableUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<DisableUserCommand>
{
    public async Task Handle(DisableUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByGuidAsync(request.Id) ?? throw new NotFoundException(nameof(User), request.Id.ToString());
        user.IsActive = false;
        await userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }
}
