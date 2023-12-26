namespace Paaralan;

sealed class CreateAdministratorVAL : Validator<CreateAdministratorAPI.Request>
{
    public CreateAdministratorVAL()
    {
        RuleFor(_ => _.Username).NotEmpty();
        RuleFor(_ => _.Password).NotEmpty();
        RuleFor(_ => _.FirstName).NotEmpty();
        RuleFor(_ => _.LastName).NotEmpty();
    }
}