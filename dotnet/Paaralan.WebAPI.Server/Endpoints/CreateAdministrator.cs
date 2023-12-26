namespace Paaralan;

sealed class CreateAdministratorEP : APIEndpoint<CreateAdministratorAPI.Request, CreateAdministrator.Request>
{
    public override void Configure()
    {
        Post("/");
        Group<EndpointGroups.Administrator>();
    }
}