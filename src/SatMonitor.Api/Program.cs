using Microsoft.EntityFrameworkCore;
using SatMonitor.Application.Interfaces;
using SatMonitor.Infrastructure.Data;
using SatMonitor.Infrastructure.Services;
using SatMonitor.Api.Middlewares;

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DataSeeder.SeedAsync(context);
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapDelete("/dev/reset", async (AppDbContext db) =>
    {
        db.Leituras.RemoveRange(db.Leituras);
        db.Sensores.RemoveRange(db.Sensores);
        db.Satelites.RemoveRange(db.Satelites);
        db.Missoes.RemoveRange(db.Missoes);
        await db.SaveChangesAsync();
        return Results.Ok(new { mensagem = "Banco limpo com sucesso" });
    });
}

app.Run();