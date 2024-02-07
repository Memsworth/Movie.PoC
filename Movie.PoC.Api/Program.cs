using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Contracts.Requests;
using Movie.PoC.Api.Database;
using Movie.PoC.Api.Entities;
using Movie.PoC.Api.Features.Films;
using Movie.PoC.Api.Features.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "movie.db");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddHttpClient<IRequestHandler<FilmService.GetFilmDataQuery, FilmDataRaw?>, FilmService.GetFilmDataHandler>(
    w => w.BaseAddress = new Uri("https://www.omdbapi.com/"));

builder.Services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidator>();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();