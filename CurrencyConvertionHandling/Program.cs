using Conversion.Domain.Models.Domain;
using Conversion.Domain.Models.Interfaces;
using Conversion_Services.Interface;
using Conversion_Services.Services;
using Conversion_SharedServices.Middleware;
using Conversion_SharedServices.Services;
using Conversion_SharedServices.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IJWTGenerationService, JWTGenerationService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        RoleClaimType = ClaimTypes.Role,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("JwtPolicy", context =>
    {
        if (context.User?.Identity?.IsAuthenticated != true)
        {
            return RateLimitPartition.GetNoLimiter("unauthenticated");
        }

        var userId =  context.User.FindFirst(ClaimTypes.Sid)?.Value;

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: $"jwt:{userId}",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            });
    });

    options.AddPolicy("AuthPolicy", context =>
    {
        var key = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: $"auth:{key}",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,   
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            });
    });
});


builder.Services.AddAuthorization();


builder.Services.AddMemoryCache();

//builder.Services.AddScoped<ICurrencyConversionProvider, FrankfurterProvider>();

builder.Services.AddHttpClient<ICurrencyConversionProvider, FrankfurterProvider>(client =>
{
    client.BaseAddress = new Uri("https://api.frankfurter.dev/v1/latest");
    client.Timeout = TimeSpan.FromSeconds(30);
});

//builder.Services.AddScoped<FrankfurterProvider>();


builder.Services.AddScoped<ICurrencyConversionProviderFactory, CurrencyConversionProviderFactory>();

builder.Services.AddScoped<ICurrencyConvertionService, CurrencyConversionService>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LogMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
