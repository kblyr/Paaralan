namespace Paaralan;

public sealed record AdministratorAlreadyExists : ICQRSErrorResponse
{
    public static readonly AdministratorAlreadyExists Instance = new();
}