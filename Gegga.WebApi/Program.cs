using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy
            .WithOrigins(config.GetValue<string>("BlazorCorsUrl") ?? throw new ArgumentNullException("BlazorCorsUrl")) // Client URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = config.GetValue<string>("B2CAuth:Authority");
    options.Audience = config.GetValue<string>("B2CAuth:Audience");
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowBlazorClient");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/securedata", [Authorize] () =>
{
    return Results.Ok("This is protected data and requires auth");
});

app.MapGet("/test", () => Results.Ok("Test works, no auth needed."));

app.Run();
