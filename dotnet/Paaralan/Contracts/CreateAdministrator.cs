namespace Paaralan;

public static class CreateAdministrator
{
    public sealed record Request : ICQRSRequest, IUserFullNameSource
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
    }

    public sealed record Response : ICQRSResponse
    {
        public int Id { get; init; }
    }
}