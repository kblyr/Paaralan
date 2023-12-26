namespace Paaralan;

sealed class CreateUserHDL : ICQRSRequestHandler<CreateUser.Request>
{
    readonly IPermissionVerifier _permissionVerifier;
    readonly IOptions<PermissionsOptions> _permissions;
    readonly IDbContextFactory<Context> _contextFactory;
    readonly IPasswordHash _passwordHash;
    readonly ICurrentAuditInfoProvider _auditInfoProvider;
    readonly IMapper _mapper;
    readonly IUserFullNameBuilder _fullNameBuilder;

    public CreateUserHDL(IPermissionVerifier permissionVerifier, IOptions<PermissionsOptions> permissions, IDbContextFactory<Context> contextFactory, IPasswordHash passwordHash, ICurrentAuditInfoProvider auditInfoProvider, IMapper mapper, IUserFullNameBuilder fullNameBuilder)
    {
        _permissionVerifier = permissionVerifier;
        _permissions = permissions;
        _contextFactory = contextFactory;
        _passwordHash = passwordHash;
        _auditInfoProvider = auditInfoProvider;
        _mapper = mapper;
        _fullNameBuilder = fullNameBuilder;
    }

    public async Task<ICQRSResponse> Handle(CreateUser.Request request, CancellationToken cancellationToken)
    {
        if (await _permissionVerifier.Verify(_permissions.Value.CreateUser, cancellationToken) == false)
        {
            return new VerifyPermissionFailed { PermissionId = _permissions.Value.CreateUser };
        }

        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        if (await context.Users.UsernameExists(request.Username, cancellationToken))
        {
            return new UsernameAlreadyExists { Username = request.Username };
        } 

        var passwordHashResult = _passwordHash.Compute(request.Password);

        if (passwordHashResult is PasswordHashComputeErrorResult || passwordHashResult is not PasswordHashComputeSuccessResult passwordHashSuccess)
        {
            return Failed.Instance;
        }

        var auditInfo = _auditInfoProvider.Value;
        var user = _mapper.Map<CreateUser.Request, User>(request) with
        {
            HashedPassword = passwordHashSuccess.HashedPassword,
            PasswordSalt = passwordHashSuccess.Salt,
            FullName = _fullNameBuilder.Build(request),
            IsAdministrator = false,
            IsPasswordChangeRequired = true,
            IsDeleted = false,
            InsertedById = auditInfo.UserId,
            InsertedOn = auditInfo.Timestamp
        };
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new CreateUser.Response { Id = user.Id };
    }
}
