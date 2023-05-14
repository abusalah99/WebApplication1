namespace WebApplication1;

public class UserUnitOfWork : BaseUnitOfWork<User>, IUserUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserUnitOfWork> _logger;
    private readonly IJwtProvider _jwtProvider;
    private readonly RefreshTokenValidator _refreshTokenValidator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly JwtRefreshOptions _jwtRefreshOptions;
    private readonly JwtAccessOptions _jwtAccessOptions;
    private readonly IImageConverter _imageConverter;

    public UserUnitOfWork(IUserRepository repository, ILogger<UserUnitOfWork> logger,
        IJwtProvider jwtProvider, RefreshTokenValidator refreshTokenValidator,
        IRefreshTokenRepository refreshTokenRepository,
        IOptions<JwtRefreshOptions> jwtRefreshOptions, 
        IOptions<JwtAccessOptions> jwtAccessOptions,
        IImageConverter converter) : base(repository, logger)
    {
        _logger = logger;
        _userRepository = repository;
        _jwtProvider = jwtProvider;
        _refreshTokenValidator = refreshTokenValidator;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtRefreshOptions = jwtRefreshOptions.Value;
        _jwtAccessOptions = jwtAccessOptions.Value;
        _imageConverter = converter;
    }

    public virtual async Task<User> GetUserByMail(string mail)
        => await _userRepository.GetByMail(mail);

    public override async Task Create(User user)
    {
        if (user == null)
            throw new ArgumentNullException("user was not provided.");

        User? userFromDb = await GetUserByMail(user.Email);
        if (userFromDb != null)
            throw new ArgumentException("this mail is already used");

        if (user.Password.Length < 5)
            throw new ArgumentException("password must be at least 6 charaters");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Role = "User";

        await base.Create(user);

    }

    public async Task<User> Update(UserRequest requestUser, Guid id)
    {
        if (requestUser == null)
            throw new ArgumentNullException("user was not provided.");

        User? userFromDb = await _userRepository.Get(id);
        if (userFromDb == null)
            throw new ArgumentException("invaild Token");

        string extention = Path.GetExtension(requestUser.UserImage.FileName);        
        byte[] image = await _imageConverter.ConvertImage(requestUser.UserImage);

        User user = new()
        {
            Id = userFromDb.Id,
            FristName = requestUser.FristName,
            LastName = requestUser.LastName,
            Password = userFromDb.Password,
            Email = requestUser.Email,
            Age = requestUser.Age,
            Phone = requestUser.Phone,
            Token = userFromDb.Token,
            Role = userFromDb.Role,
            ImageExtention = extention,
            UserImage = image,
        };

        await Update(user);

        return user;
    }

    public async Task DeleteUserByMail(string mail)
    {
        using IDbContextTransaction transaction = await _userRepository.GetTransaction();
        try
        {
            await _userRepository.DeleteByMail(mail);
        }
        catch (Exception exception)
        {
            transaction.Rollback();

            _logger.LogError(exception.Message);
        }
        await transaction.CommitAsync();
    }

    public async Task<Token> Login(LoginRequest request)
    {
        User? userFromDb = await GetUserByMail(request.Email);

        if (userFromDb == null)
            throw new ArgumentException("user was not found");

        if (!BCrypt.Net.BCrypt.Verify(request.password, userFromDb.Password))
            throw new ArgumentException("wrong password");

        if (userFromDb.Token == null)
            userFromDb.Token = CreateNewRefreshToken(userFromDb.Id);

        if(!_refreshTokenValidator.Validate(userFromDb.Token.Value))
            userFromDb.Token = CreateNewRefreshToken(userFromDb.Id, userFromDb.Token.Id);

        await Update(userFromDb);    

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.Token.Value,
            RefreshTokenExpiresAtExpires = userFromDb.Token.ExpireAt
        };

        return token;
    }

    public async Task<Token> Register(User user)
    {
        user.Token = CreateNewRefreshToken();
        user.CreatedAt = DateTime.UtcNow;

        await this.Create(user);

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(user),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = user.Token.Value,
            RefreshTokenExpiresAtExpires = user.Token.ExpireAt
        };

        return token;
    }

    public async Task<Token> Refresh(string refreshToken)
    {
        User userFromDb = await _userRepository.GetByToken(refreshToken);

        if (userFromDb == null)
            throw new ArgumentException("Invalid Token");

        Token token = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.Token.Value,
            RefreshTokenExpiresAtExpires = userFromDb.Token.ExpireAt
        };

        return token;
    }

    public async Task Logout(string refreshToken)
    {
        User userFromDb = await _userRepository.GetByToken(refreshToken);
        if (userFromDb == null)
            throw new ArgumentException("Invalid Token");

        using IDbContextTransaction transaction = await _refreshTokenRepository.GetTransaction();
        try
        {
            await _refreshTokenRepository.Remove(userFromDb.Token.Id);
        }
        catch (Exception exception)
        {
            transaction.Rollback();

            _logger.LogError(exception.Message);
        }
        await transaction.CommitAsync();
    }

    public async Task<Token> UpdatePassword(PasswordRequest password, Guid id)
    {
        User userFromDb = await _userRepository.Get(id);

        if (userFromDb == null)
            throw new ArgumentException("User not found");
        if (!BCrypt.Net.BCrypt.Verify(password.Password, userFromDb.Password))
            throw new ArgumentException("wrong password");

        if (password.NewPassword == null)
            throw new ArgumentException("new password can not be null");

        userFromDb.Password = BCrypt.Net.BCrypt.HashPassword(password.NewPassword);

        userFromDb.Token = CreateNewRefreshToken(userFromDb.Id, userFromDb.Token.Id);

        await Update(userFromDb);

        Token newToken = new()
        {
            AccessToken = _jwtProvider.GenrateAccessToken(userFromDb),
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtAccessOptions.ExpireTimeInMintes),
            RefreshToken = userFromDb.Token.Value,
            RefreshTokenExpiresAtExpires = userFromDb.Token.ExpireAt
        };

        return newToken;
    }

    private RefreshToken CreateNewRefreshToken(Guid userId = default(Guid)
        ,Guid id = default(Guid))
    {
    string refreshToken = _jwtProvider.GenrateRefreshToken();

    RefreshToken newRefreshToken = new()
    {
        Id = id,
        Value = refreshToken,
        CreatedAt = DateTime.UtcNow,
        ExpireAt = DateTime.UtcNow.AddMonths(_jwtRefreshOptions.ExpireTimeInMonths),
        UserId = userId
    };

        return newRefreshToken;
    } 
}
