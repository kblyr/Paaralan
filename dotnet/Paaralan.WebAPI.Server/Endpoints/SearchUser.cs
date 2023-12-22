namespace Paaralan;

sealed class SearchUserEP : APIEndpoint<SearchUserAPI.Request, SearchUser.Request>
{
    public override void Configure()
    {
        Get("/search");
        Group<EndpointGroups.User>();
    }
}