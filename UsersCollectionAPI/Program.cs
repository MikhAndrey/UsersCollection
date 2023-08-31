using Microsoft.AspNetCore.Authentication;
using UsersCollectionAPI.Extensions;
using UsersCollectionAPI.Services;
using UsersCollectionAPI.Utils;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

builder.Services.AddDatabaseServices(configuration);
builder.Services.AddUserServices();
builder.Services.AddCommands();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Services.GetRequiredService<UserCacheService>().Init();

app.Run();
