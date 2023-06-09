using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                                                                      .EnableDetailedErrors()
                                                                      .EnableSensitiveDataLogging()
                                                                      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddOptions();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Services.ConfigureOptions<JwtAccessOptionsSetup>();
builder.Services.ConfigureOptions<JwtRefreshOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseUnitOfWork<>), typeof(BaseUnitOfWork<>));
builder.Services.AddScoped(typeof(IBaseRepositiorySettings<>), typeof(BaseRepositiorySettings<>));
builder.Services.AddScoped(typeof(IBaseUnitOfWork<>), typeof(BaseSettingsUnitOfWork<>));

builder.Services.AddScoped<IBookRepsitory, BookRepository>();
builder.Services.AddScoped<IBookUnitOfWork, BookUnitOfWork>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


builder.Services.AddSingleton<IImageConverter, ImageConverter>();
builder.Services.AddSingleton<IJwtProvider, JwtProvider>();

builder.Services.AddTransient<GlobalErrorHandlerMiddleware>();
builder.Services.AddTransient<TransactionRollbackMiddleware>();
builder.Services.AddTransient<RefreshTokenValidator>();
builder.Services.AddTransient<CorsMiddleware>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseMiddleware<CorsMiddleware>();
app.UseMiddleware<TransactionRollbackMiddleware>();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
