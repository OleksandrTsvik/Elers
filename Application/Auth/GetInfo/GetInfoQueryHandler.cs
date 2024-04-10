using Application.Common.Interfaces;
using Application.Common.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.GetInfo;

public class GetInfoQueryHandler : IQueryHandler<GetInfoQuery, InfoResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserContext _userContext;
    private readonly IUserErrors _userErrors;

    public GetInfoQueryHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IUserErrors userErrors)
    {
        _context = context;
        _userContext = userContext;
        _userErrors = userErrors;
    }

    public async Task<Result<InfoResponse>> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == _userContext.UserId, cancellationToken);

        if (user is null)
        {
            return _userErrors.NotFoundByUserContext();
        }

        return new InfoResponse
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
