namespace Paaralan;

sealed class CreateUserVAL : Validator<CreateUserAPI.Request>
{
    public CreateUserVAL()
    {
        RuleFor(_ => _.Username).NotEmpty();
        RuleFor(_ => _.Password).NotEmpty();
        RuleFor(_ => _.FirstName).NotEmpty();
        RuleFor(_ => _.LastName).NotEmpty();
    }
}