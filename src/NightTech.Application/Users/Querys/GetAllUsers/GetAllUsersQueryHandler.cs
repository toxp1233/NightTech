using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NightTech.Application.common;
using NightTech.Application.PublicDtos;
using NightTech.Domain.Interfaces;

namespace NightTech.Application.Users.Querys.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = userRepository.GetQueryable();

        if (!string.IsNullOrWhiteSpace(request.PaginationParams.Search))
        {
            var search = request.PaginationParams.Search.ToLower();
            users = users
                .Where(p => p.UserName != null &&
                            p.UserName.ToLower().Contains(search));
        }

        var projected = users.ProjectTo<UserDto>(mapper.ConfigurationProvider);

        return await PaginatedList<UserDto>.CreateAsync(
            projected,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize
        );
    }
}
