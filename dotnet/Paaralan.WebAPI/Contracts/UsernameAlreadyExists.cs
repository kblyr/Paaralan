namespace Paaralan;

[SchemaId(SchemaIds.UsernameAlreadyExists)]
public sealed record UsernameAlreadyExistsAPI : IAPIErrorResponse
{
    public required string Username { get; init; }
}