using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RegisterAPI.Application;
using RegisterAPI.Application.Interface;
using RegisterAPI.Authorization;
using RegisterAPI.CrossCutting.ExternalService;
using RegisterAPI.CrossCutting.ExternalService.Interface;
using RegisterAPI.CrossCutting.Mapper;
using RegisterAPI.CrossCutting.QueueService;
using RegisterAPI.CrossCutting.QueueService.Interface;
using RegisterAPI.Repository.Context;
using RegisterAPI.Repository.Interface;
using RegisterAPI.Repository.Repository;
using RegisterAPI.Service;
using RegisterAPI.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();

#region App
builder.Services.AddTransient<IClientApp, ClientApp>();
builder.Services.AddTransient<INotificationApp, NotificationApp>();
builder.Services.AddTransient<ILoggerApp, LoggerApp>();
#endregion

#region Service
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<ILogService, LogService>();
builder.Services.AddTransient<ISendMessageService, SendMessageService>();
#endregion

#region Repository
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<ILogRepository, LogRepository>();
#endregion

builder.Services.AddDbContext<RegisterAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<LogContext>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongodbConnection");
    var databaseName = builder.Configuration["MongoDB:DatabaseName"];
    return new LogContext(connectionString, databaseName);
});

builder.Services.AddAuthorization(authConfig =>
{
    authConfig.AddPolicy("XApiKey",
        policyBuilder => policyBuilder
            .AddRequirements(new ApiKeyRequirement()));
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme()
    {
        Description = "API Key Authentication",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-API-KEY"
                }
            },
            Array.Empty<string>()
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Register API",
        Description = "Main Register API"
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
