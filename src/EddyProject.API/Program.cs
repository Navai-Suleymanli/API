using Eddyproject.Business;
using Eddyproject.Common.Interfaces;
using Eddyproject.Common.Model;
using Eddyproject.Infrastructure;
using EddyProject.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, provider, config) => config.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day));


// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);
var dbFilename = Environment.GetEnvironmentVariable("DB_FILENAME");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Filename = {dbFilename}")); 
builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();
builder.Services.AddScoped<IGenericRepository<Budget>, GenericRepository<Budget>>();
builder.Services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
builder.Services.AddScoped<IGenericRepository<Course>, GenericRepository<Course>>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var authUrl = Environment.GetEnvironmentVariable("AUTH_URL");
var tokenUrl = Environment.GetEnvironmentVariable("TOKEN_URL");
var oauthScope = Environment.GetEnvironmentVariable("SCOPE");


builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("oauth", new OpenApiSecurityScheme()
    {
        Description = "Auth code + PKCE",
        Name = "oauth",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            AuthorizationCode = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri(authUrl),
                TokenUrl = new Uri( tokenUrl),
                Scopes = new Dictionary<string, string>()
                {
                    {oauthScope, "Access the API" }
                }
            }
        }
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference(){Type = ReferenceType.SecurityScheme, Id = "oauth"}
            },
            new List<string> (){oauthScope}
        }
    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("http://example.com",
                                "http://www.contoso.com");
        });

    options.AddPolicy("AnotherPolicy",
        policy =>
        {
            policy.WithOrigins("http://www.contoso.com")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
var clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
var adDomainn = Environment.GetEnvironmentVariable("AD_DOMAIN");
var identityInstance = Environment.GetEnvironmentVariable("IDENTITY_INSTANCE");
var tenantId = Environment.GetEnvironmentVariable("TENANT_ID");
var callBackPath = Environment.GetEnvironmentVariable("CALLBACK_PATH");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi((bearerOptions) 
    => { }, (miOptions) => 
    {
        miOptions.ClientId = clientId;
        miOptions.ClientSecret = clientSecret;
        miOptions.Domain = adDomainn;
        miOptions.Instance = identityInstance;
        miOptions.TenantId = tenantId;
        miOptions.CallbackPath = callBackPath;
    
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.OAuthClientId(clientId);
    o.OAuthUsePkce();
    o.OAuthScopeSeparator(" ");

});


using(var scope = app.Services.CreateScope()) // scope artirmaq esasdir databasecontext artirandan sonra
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
