using Microsoft.Extensions.Options;
using Notification.Domain.Interface;
using Notification.Infrasturcture.DTOModel;
using Notification.Infrasturcture.Repositories;
using Notification.Infrasturcture.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Notification.Infrasturcture.Context.NotificationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("NoticationConnection")));


builder.Services.AddHostedService<NotificationRabbitMQService>();
// Add services to the container.
builder.Services.Configure<ConfigurationStorageOptions>(builder.Configuration.GetSection("ConfigurationSettings"));

builder.Services.AddScoped<IConfigurationDetails>(provider =>
{
    var configurationStorageOptions = provider.GetRequiredService<IOptions<ConfigurationStorageOptions>>().Value;

    if (configurationStorageOptions.UseAzure)
    {
        return provider.GetRequiredService<ConfigurationDetailAzureRepository>();
    }
    else
    {
        return provider.GetRequiredService<ConfigurationDetailRepository>();
    }

});

var app = builder.Build();

// Configure the HTTP request pipeline.


app.Run();

