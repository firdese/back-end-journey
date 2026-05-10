using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskTracker.API.Filters;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Application.Services;
using TaskTracker.Infrastructure.Persistence;
using TaskTracker.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        policy => {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddDbContext<WebAPIDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskGroupRepository, TaskGroupRepository>();
builder.Services.AddScoped<ITaskGroupService, TaskGroupService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpContextAccessor();

// Register AutoMapper - scans assemblies for Profile classes
builder.Services.AddAutoMapper(_ => { }, typeof(TaskTracker.Application.Mapping.MappingProfile));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options => {
        options.Authority = builder.Configuration.GetValue<string>("Authentication:Authority");
        options.RequireHttpsMetadata = builder.Configuration.GetValue("Authentication:RequireHttpsMetadata", true);

        options.TokenValidationParameters = new TokenValidationParameters {
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:ValidIssuer")
                            ?? options.Authority,
            ValidateIssuer = true,
            ValidateAudience = false
        };

        // Add event handlers
        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents {
            OnAuthenticationFailed = context => {
                Console.WriteLine("❌ Authentication failed: " + context.Exception.Message);
                if (context.Exception.InnerException != null)
                    Console.WriteLine("   Inner: " + context.Exception.InnerException.Message);
                return Task.CompletedTask;
            },
            OnChallenge = context => {
                Console.WriteLine("⚠️ Challenge error: " + context.Error + " - " + context.ErrorDescription);
                return Task.CompletedTask;
            },
            OnTokenValidated = context => {
                Console.WriteLine("✅ Token validated successfully");
                return Task.CompletedTask;
            },
            OnMessageReceived = context => {
                Console.WriteLine("📩 Token received: " + context.Token);
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization(); ;


var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<WebAPIDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment()) {
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
