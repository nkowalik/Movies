using Movies.Api.DbContexts;
using Movies.Api.Profiles;
using Movies.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.SuppressAsyncSuffixInActionNames = false;
}).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

builder.Services.AddDbContext<AMoviesContext>(
    ServiceLifetime.Singleton);
builder.Services.AddDbContext<BMoviesContext>(
    ServiceLifetime.Singleton);
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API");
});

app.UseHttpsRedirection();
app.UseRouting();

app.Run();