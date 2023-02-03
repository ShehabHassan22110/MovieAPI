using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieBestAPI.Helper;
using MovieBestAPI.Interfaces;
using MovieBestAPI.Models;
using MovieBestAPI.Services;
//using MovieBestAPI.Helper;
//using MovieBestAPI.Interfaces;
//using MovieBestAPI.Services;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Authentications && JWT ----------------------








//-------------------------------------
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// data 
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSetings"));
builder.Configuration.GetSection("JWT");
builder.Services.AddScoped<IGenreService, GenreServices>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<ITvShowService, TvShowService>();
builder.Services.AddTransient<IMailService, MailService>();
//builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefultConnection")));
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();

app.UseAuthorization();
app.UseCors(options => options
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
app.MapControllers();

app.Run();
