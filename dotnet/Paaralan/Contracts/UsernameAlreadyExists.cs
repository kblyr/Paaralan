namespace Paaralan;

public sealed record UsernameAlreadyExists : ICQRSErrorResponse
{
    public required string Username { get; init; }
}