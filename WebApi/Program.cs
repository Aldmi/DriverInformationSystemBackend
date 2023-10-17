using System.Security.Claims;
using System.Text;
using Application;
using Application.Common.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Extensions;
using WebApi.MIddleware;
using WebApi.Settings;
using WebApi.WebApiServices;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();
Log.Information("{Version}", VersionService.GetVersion());

try
{
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    var builder = WebApplication.CreateBuilder(args);
    var appConfiguration = builder.Configuration;
    var env = builder.Environment;

    //Serilog------------------------------------------------------------------------------------------------
    var loggerSettings = SettingsFactory.GetLoggerConfig(env, appConfiguration);
    Log.Information("{LoggerSettings}", loggerSettings!.ToString());
    builder.Services.AddSerilogServices(loggerSettings, env);
    builder.Host.UseSerilog();
    
    
    // Add services to the container.
    builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthJwtOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthJwtOptions.AUDIENCE,
                IssuerSigningKey = AuthJwtOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                
                ValidateLifetime = true,
                LifetimeValidator = (_, expires, _, _) =>
                {
                    if (expires != null)
                    {
                        return expires > DateTime.UtcNow;
                    }
                    return false;
                }
            };
        });
    builder.Services.AddAuthorization();  

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "VSA Todo API", Version = "v1" }));

    builder.Services.AddProblemDetails();

    builder.Services.AddCors(options => options.AddDefaultPolicy(
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

    builder.Services.AddHealthChecks();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddApplication(Log.Logger);
    builder.Services.AddPersistence(SettingsFactory.GetDbConnectionString(env, appConfiguration), Log.Logger);


    var app = builder.Build();

    app.UseCors();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        
        app.UseExceptionHandler("/error-development");
    }
    else
    {
        app.UseExceptionHandler("/error");
    }

    app.ConfigureCustomExceptionMiddleware();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    //End point - проверка авторизации
    app.Map("/authTest", [Authorize](HttpContext context)  =>
    {
        var user = context.User.Identity;
        if (user is not null && user.IsAuthenticated)
        {
            var login = context.User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
            var role = context.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType); 
            return Results.Json(new
            { 
                Auth=true,
                Login=login?.Value,
                Role= role?.Value
            });
        }
        return Results.Json(new
        { 
            Auth=false
        });
    });

    //End point - проверка работоспособности
    app.MapGet("_version", () => VersionService.GetVersion());

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
