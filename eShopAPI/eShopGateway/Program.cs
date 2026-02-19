using JWTAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddCustomJwtAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();

app.UseAuthorization();

app.Run();
