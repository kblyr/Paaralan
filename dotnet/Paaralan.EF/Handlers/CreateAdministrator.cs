namespace Paaralan;

sealed class CreateAdministratorHDL : ICQRSRequestHandler<CreateAdministrator.Request>
{
    readonly IDbContextFactory<Context> _contextFactory;
    readonly IPasswordHash _passwordHash;
    readonly IMapper _mapper;
    readonly IUserFullNameBuilder _fullNameBuilder;

    public CreateAdministratorHDL(IDbContextFactory<Context> contextFactory, IPasswordHash passwordHash, IMapper mapper, IUserFullNameBuilder fullNameBuilder)
    {
        _contextFactory = contextFactory;
        _passwordHash = passwordHash;
        _mapper = mapper;
        _fullNameBuilder = fullNameBuilder;
    }

    public async Task<ICQRSResponse> Handle(CreateAdministrator.Request request, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        if (await context.Users.Where(user => user.IsAdministrator && (user.IsDeleted == false)).AnyAsync(cancellationToken))
        {
            return AdministratorAlreadyExists.Instance;
        }

        if (await context.Users.UsernameExists(request.Username, cancellationToken))
        {
            return new UsernameAlreadyExists { Username = request.Username };
        }

        var passwordHashResult = _passwordHash.Compute(request.Password);

        if (passwordHashResult is PasswordHashComputeErrorResult || passwordHashResult is not PasswordHashComputeSuccessResult passwordHashSuccess)
        {
            return Failed.Instance;
        }

        var user = _mapper.Map<CreateAdministrator.Request, User>(request) with
        {
            HashedPassword = passwordHashSuccess.HashedPassword,
            PasswordSalt = passwordHashSuccess.Salt,
            FullName = _fullNameBuilder.Build(request),
            IsAdministrator = true,
            IsPasswordChangeRequired = false,
            IsDeleted = false
        };
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new CreateAdministrator.Response { Id = user.Id };
    }
}
