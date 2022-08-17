using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DisneyApi.Services.DbService;
using DisneyApi.Repositories;
using DisneyApi.Contexts;
using DisneyApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Moddified Swagger to allow authentication.
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition(
        name: "Bearer", 
        securityScheme: new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingrese Bearer [Token]  para poder autentificarse dentro de la aplicacion."
        });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                 Type = ReferenceType.SecurityScheme,
                 Id = "Bearer"
                }

            },
            new List<string>()
        }
    });
});

//              Set DDBB Contexts:

var usersDb_ConnString = builder.Configuration.GetConnectionString("UserDbConnection");
builder.Services.AddDbContext<UserContext>(x => x.UseSqlServer(usersDb_ConnString));

var disneyDb_ConnString = builder.Configuration.GetConnectionString("DisneyDbConnection");
builder.Services.AddDbContext<DisneyDbContext>(x => x.UseSqlServer(disneyDb_ConnString));

//              Set Services:

builder.Services.AddScoped<DisneyRepository<Character>>();

builder.Services.AddScoped<DisneyService<Character>>();

builder.Services.AddScoped<DisneyRepository<Genre>>();

builder.Services.AddScoped<DisneyService<Genre>>();

builder.Services.AddScoped<DisneyRepository<MovieSerie>>();

builder.Services.AddScoped<DisneyService<MovieSerie>>();

//              Authentication:

// DI to implement the Login system
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

// DI to Configure token and Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false; // required in production environment
    //option.Authority = "https://localhost:7158";
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "https://localhost:7158",
        ValidIssuer = "https://localhost:7158",
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTRefreshTokenHIGHsecuredPasswordVVVp1OH7Xzyr"))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Important    
//    \/ 
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
