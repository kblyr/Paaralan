namespace Paaralan;

public sealed record PermissionsOptions
{
    public const string CONFIGKEY = "Paaralan:Permissions";
    
    public int CreateUser { get; set; } = 1;
}