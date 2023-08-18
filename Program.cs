using Microsoft.EntityFrameworkCore;
using SimpleMcRecords.Data;
using SimpleMcRecords.Dto;
using SimpleMcRecords.Helper;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;
using SimpleMcRecords.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));
// Add repositories
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IAdvancementRepository, AdvancementRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IScoreRepository, ScoreRepository>();
// Add mappers
builder.Services.AddScoped<IMapper<Player, PlayerDto>, PlayerMapper>();
builder.Services.AddScoped<IMapper<Advancement, AdvancementDto>, AdvancementMapper>();
builder.Services.AddScoped<IMapper<Category, CategoryDto>, CategoryMapper>();
builder.Services.AddScoped<IMapper<Score, ScoreDto>, ScoreMapper>();
builder.Services.AddScoped<IMapper<Score, FullScoreDto>, FullScoreMapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

// Migrating database
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}

app.Run();
