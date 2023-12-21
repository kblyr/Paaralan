namespace Paaralan;

static class DbSetExtensions
{
    public static Task<bool> UsernameExists(this DbSet<User> users, string username, CancellationToken cancellationToken = default)
    {
        return users
            .Where(user => user.Username == username && (user.IsDeleted == false))
            .AnyAsync(cancellationToken);
    }
}