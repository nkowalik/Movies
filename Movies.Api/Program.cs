using Movies.Api.Infrastructure.DbContexts;
using Movies.Api.Infrastructure.Repositories;
using Movies.Api.Profiles;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Movies.Api.DataCollectors;
using Movies.Api.Infrastructure.Entities;

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
    setupAction.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Movies API",
        Description = "An API for displaying movies information from two sources:\n" +
        "OmDb - The Open Movie Database, api key required.\n" +
        "FakeDb - small database with only 21 movies, but api key is not required.",
    });
});

builder.Services.AddDbContext<MoviesContext>(
    dbContextOpts => dbContextOpts.UseSqlite(
        builder.Configuration["ConnectionStrings:MoviesDBConnectionString"]), 
    ServiceLifetime.Singleton);
builder.Services.AddHttpClient();
builder.Services.AddScoped<IMoviesRepository<OmDbMovieEntity>, OmDbMoviesRepository>();
builder.Services.AddScoped<IMoviesRepository<FakeDbMovieEntity>, FakeDbMoviesRepository>();
builder.Services.AddScoped<IMoviesDataCollector, MoviesDataCollector>();
builder.Services.AddAutoMapper(typeof(OmDbMoviesProfile));
builder.Services.AddAutoMapper(typeof(FakeDbMoviesProfile));
builder.Services.AddAutoMapper(typeof(FakeDbMovieDetailsProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API");
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();