using Microsoft.EntityFrameworkCore;
using SatMonitor.Application.Interfaces;
using SatMonitor.Infrastructure.Data;
using SatMonitor.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddScoped<IMissaoService, MissaoService>();
builder.Services.AddScoped<ISateliteService, SateliteService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<ILeituraSensorService, LeituraSensorService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();