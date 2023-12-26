using Mapster;

namespace Paaralan;

sealed class Mapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AdministratorAlreadyExists, AdministratorAlreadyExistsAPI>();

        config.ForType<CreateAdministrator.Response, CreateAdministratorAPI.Response>()
            .Map(dest => dest.Id, src => HashIdConverterInstance.Instance.FromInt32(src.Id));

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
            .RegisterBadRequest<AdministratorAlreadyExists, AdministratorAlreadyExistsAPI>()
            .RegisterCreated<CreateAdministrator.Response, CreateAdministratorAPI.Response>()
            .RegisterCreated<CreateUser.Response, CreateUserAPI.Response>()
            .RegisterOK<SearchUser.Response, SearchUserAPI.Response>()
            .RegisterBadRequest<UsernameAlreadyExists, UsernameAlreadyExistsAPI>()
        ;
    }
}
