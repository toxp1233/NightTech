using MediatR;
using NightTech.Domain.Entities;
using NightTech.Domain.Exceptions;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Auth.Commands.Logout;

public class LogoutCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByGuidAsync(request.Id) ?? throw new NotFoundException(nameof(User), request.Id.ToString());
        user.RefreshToken = null;
        user.RefreshTokenExpireDate = null;
        await unitOfWork.SaveChangesAsync();
    }
}
