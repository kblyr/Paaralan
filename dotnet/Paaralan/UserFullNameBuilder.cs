namespace Paaralan;

public interface IUserFullNameSource
{
    string FirstName { get; }
    string LastName { get; }
}

public interface IUserFullNameBuilder
{
    string Build(string firstName, string lastName);
    string Build(IUserFullNameSource source);
}