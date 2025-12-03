using MediatR;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Users.Commands.AssignRole;

public class AssignRoleCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<AssignRoleCommand>
{
    public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByGuidAsync(request.Id) ?? throw new Domain.Exceptions.NotFoundException(nameof(Domain.Entities.User), request.Id.ToString());
        user.Role = request.Role;
        user.UpdatedAt = DateTime.UtcNow;
        await userRepository.Update(user);
        await unitOfWork.SaveChangesAsync();
    }
}
