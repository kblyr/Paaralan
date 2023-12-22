using Mapster;

namespace Paaralan;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateUser.Response, CreateUserAPI.Response>()
            .Map(dest => dest.Id, src => HashIdConverterInstance.Instance.FromInt32(src.Id));

        config.ForType<SearchUser.Response.UserObj, SearchUserAPI.Response.UserObj>()
            .Map(dest => dest.Id, src => HashIdConverterInstance.Instance.FromInt32(src.Id));
    }
}

sealed class ResponseTypeMapRegistration : IResponseTypeMapRegistration
{
    public void Register(IResponseTypeMapRegistry registry)
    {
        registry
            .RegisterCreated<CreateUser.Response, CreateUserAPI.Response>()
            .RegisterOK<SearchUser.Response, SearchUserAPI.Response>()
            .RegisterBadRequest<UsernameAlreadyExists, UsernameAlreadyExistsAPI>()
        ;
    }
}
