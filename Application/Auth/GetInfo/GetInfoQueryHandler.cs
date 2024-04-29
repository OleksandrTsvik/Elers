using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.GetInfo;

public class GetInfoQueryHandler : IQueryHandler<GetInfoQuery, GetInfoResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;

    public GetInfoQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<Result<GetInfoResponse>> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == _userContext.UserId, cancellationToken);

        if (user is null)
        {
            return UserErrors.NotFoundByUserContext();
        }

        return new GetInfoResponse
        {
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Patronymic = user.Patronymic,
            AvatarUrl = user.AvatarUrl,
            BirthDate = user.BirthDate
        };
    }
}
