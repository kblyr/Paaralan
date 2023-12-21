namespace Paaralan;

sealed class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
}