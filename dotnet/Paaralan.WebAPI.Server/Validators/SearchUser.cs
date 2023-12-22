namespace Paaralan;

sealed class SearchUserVAL : Validator<SearchUserAPI.Request>
{
    public SearchUserVAL()
    {
        RuleFor(_ => _.PageSize).GreaterThan(0);
    }
}