namespace Paaralan;

sealed class CreateUserEP : APIEndpoint<CreateUserAPI.Request, CreateUser.Request>
{
    public override void Configure()
    {
        Post("/");
        Group<EndpointGroups.User>();
    }
}