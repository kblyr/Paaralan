namespace Paaralan;

public static class SearchUser
{
    public sealed record Request : ICQRSRequest
    {
        public required string Query { get; init; }
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
    }

    public sealed record Response : ICQRSResponse
    {
        public required UserObj[] Users { get; init; }
        public int PageCount { get; init; }

        public sealed record UserObj
        {
            public int Id { get; init; }
            public required string Username { get; init; }
            public required string FullName { get; init; }
            public bool IsAdministrator { get; init; }
        }
    }
}