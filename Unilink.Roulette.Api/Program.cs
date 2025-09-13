using Microsoft.EntityFrameworkCore;
using Unilink.Roulette.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// EF Core + SQLite (cambiable a SQL Server si quer�s)
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("sqlite")));

builder.Services.AddControllers(); // MVC
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

