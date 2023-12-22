namespace Paaralan;

public static class SearchUserAPI
{
    public sealed record Request : IAPIRequest
    {
        [BindFrom("q")]
        public string Query { get; init; } = "";
        
        [BindFrom("pg")]
        public int PageIndex { get; init; }
        
        [BindFrom("sz")]
        public int PageSize { get; init; }
    }

    [SchemaId(SchemaIds.SearchUser)]
    public sealed record Response : IAPIResponse
    {
        public required UserObj[] Users { get; init; }
        public int PageCount { get; init; }

        public sealed record UserObj
        {
            public required string Id { get; init; }
            public required string Username { get; init; }
            public required string FullName { get; init; }
        }
    }
}