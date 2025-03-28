using AutoMapper;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities.RBAC;
using BookLibraryAPIDemo.Infrastructure.Context;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Application.Queries.Permissions;

public class GetUserPermissionsQuery : IRequest<GetUserPermissionsResponse>
{
    public required string Id { get; set; }
}

public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, GetUserPermissionsResponse>
{
    private readonly BookLibraryContext _context;

    public GetUserPermissionsQueryHandler(BookLibraryContext context)
    {
        _context = context;
    }

    public async Task<GetUserPermissionsResponse> Handle(GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        if (user == null)
        {
            throw new NotFoundException($"User with id '{request.Id}' not found.");
        }

        var permissions = user.Roles.SelectMany(r => r.Permissions).Select(p => p.Name)
            .ToHashSet();
        return new GetUserPermissionsResponse(permissions);
    }
}

public record GetUserPermissionsResponse(HashSet<string> Permissions);