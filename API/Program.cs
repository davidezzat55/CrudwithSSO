using API.PresentationBuilder;
using ApplicationServices.ApplicationServicesBuilder;
using ApplicationServices.Mapper;
using ApplicationServices.Services;
using Core.DominModels.UserAggregate;
using Infrastructure.Context;
using Infrastructure.InfrastructureBuilder;
using Infrastructure.Repositories;
using LinkDev.Wasel.Core.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddPresentation(builder.Configuration)
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

builder.Logging.AddDBLogging(builder.Configuration);


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = "https://your-adfs-server/adfs"; 
    options.ClientId = "your-client-id"; 
    options.ResponseType = "id_token"; 
    options.CallbackPath = "/signin-oidc"; 
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.SaveTokens = true;
});

builder.Services.AddControllersWithViews();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
