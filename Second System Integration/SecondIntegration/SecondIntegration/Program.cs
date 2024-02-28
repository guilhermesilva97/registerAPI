using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using SecondIntegration.Application;
using SecondIntegration.Application.Interface;
using SecondIntegration.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();
builder.Services.AddTransient<IClientApp, ClientApp>();

builder.Services.AddControllers();

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
        Title = "Second Integration API",
        Description = "Second Integration API"
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
