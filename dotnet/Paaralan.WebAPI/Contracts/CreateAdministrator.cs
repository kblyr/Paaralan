namespace Paaralan;

public static class CreateAdministratorAPI
{
    public sealed record Request : IAPIRequest
    {
        public string Username { get; init; } = "";
        public string Password { get; init; } = "";
        public string FirstName { get; init; } = "";
        public string LastName { get; init; } = "";
    }

    [SchemaId(SchemaIds.CreateAdministrator)]
    public sealed record Response : IAPIResponse
    {
        public required string Id { get; init; }
    }
}