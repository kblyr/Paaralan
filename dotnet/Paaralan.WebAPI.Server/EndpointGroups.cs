namespace Paaralan;

static class EndpointGroups
{
    public sealed class User : Group
    {
        public User()
        {
            Configure("/users", ep => {});
        }
    }
}