using Notification.Infrasturcture.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<NotificationRabbitMQService>();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.


app.Run();

