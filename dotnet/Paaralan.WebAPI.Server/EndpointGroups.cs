namespace Paaralan;

static class EndpointGroups
{
    public sealed class Administrator : Group
    {
        public Administrator()
        {
            Configure("/administrator", ep => ep.AllowAnonymous());
        }
    }

    public sealed class User : Group
    {
        public User()
        {
            Configure("/users", ep => {});
        }
    }
}