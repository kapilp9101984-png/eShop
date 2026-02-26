using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;
using Auth.Infrastructure.Repositories;
using JWTAuthenticationManager;
using MediatR;
using Auth.Application.Operation.Command;
using Microsoft.EntityFrameworkCore;
using Auth.Infrastructure.Services;
using Auth.Application.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core DbContext for SQLite. Uses connection string "AuthConnection" from configuration
// or falls back to a local file `auth.db` when not provided.
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUser,UserRepository >();
builder.Services.AddScoped<IRole,RoleRepository >();
builder.Services.AddScoped<IFeatures,FeaturesRepository >();
builder.Services.AddScoped<IUserInRole,UserInRoleRepository >();
builder.Services.AddScoped<IFeatureInRole,FeatureInRoleRepository >();
builder.Services.AddScoped<IRefreshToken,RefreshTokenRepository >();
builder.Services.AddScoped<IPasswordResetTokens,PasswordResetTokensRepository >();
builder.Services.AddScoped<IEmailVerificationTokens,EmailVerificationTokensRepository >();
builder.Services.AddScoped<IMapper,Mapper>();
builder.Services.AddScoped<IEncryption,Encryption>();


builder.Services.AddSingleton<JWTTokenHandler>();
// Register MediatR handlers from the Auth.Application assembly
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUser).Assembly));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();

