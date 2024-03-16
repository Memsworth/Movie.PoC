using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movie.PoC.Api.Contracts;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Features.Auth;
using Movie.PoC.Api.Features.FilmsData;
using Movie.PoC.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "movie.db");

builder.Services.Configure<OmDbSettings>(builder.Configuration.GetSection(nameof(OmDbSettings)));

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Key").Value!);
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false, //dev
            ValidateAudience = false, //dev
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = false, //dev
            ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
            ValidAudience = builder.Configuration["JwtConfig:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });

builder.Services.AddHttpClient(client => client.BaseAddress = new Uri("https://www.omdbapi.com/"));

builder.Services.AddScoped<IValidator<CreateUserRequest>, RegisterUserCommandValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginQueryValidation>();
builder.Services.AddScoped<IValidator<string>, GetFilmDataQueryValidation>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
var app = builder.Build();

// Configure the HTTP request pipeline.
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