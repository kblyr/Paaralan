using Mapster;

namespace Paaralan;

sealed class SearchUserHDL : ICQRSRequestHandler<SearchUser.Request>
{
    readonly IDbContextFactory<Context> _contextFactory;

    public SearchUserHDL(IDbContextFactory<Context> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<ICQRSResponse> Handle(SearchUser.Request request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var query = context.Users
            .Where(user => user.IsDeleted == false)
            .WhereIf(user => user.Username.Contains(request.Query) || user.FullName.Contains(request.Query), !string.IsNullOrWhiteSpace(request.Query));
        return new SearchUser.Response
        {
            Users = await query
                .WithPagination(request.PageIndex, request.PageSize)
                .ProjectToType<SearchUser.Response.UserObj>()
                .ToArrayAsync(cancellationToken),
            PageCount = await query.CountPage(request.PageSize, cancellationToken)
        };
    }
}
