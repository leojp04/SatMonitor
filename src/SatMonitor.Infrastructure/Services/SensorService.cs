using Microsoft.EntityFrameworkCore;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;
using SatMonitor.Domain.Entities;
using SatMonitor.Domain.Enums;
using SatMonitor.Infrastructure.Data;

namespace SatMonitor.Infrastructure.Services;

public class SensorService : ISensorService
{
    private readonly AppDbContext _context;

    public SensorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SensorDto>> GetAllAsync()
    {
        return await _context.Sensores
            .Select(s => new SensorDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Tipo = s.Tipo.ToString(),
                Unidade = s.Unidade,
                LimiteMin = s.LimiteMin,
                LimiteMax = s.LimiteMax,
                SateliteId = s.SateliteId
            }).ToListAsync();
    }

    public async Task<SensorDto?> GetByIdAsync(int id)
    {
        var s = await _context.Sensores.FindAsync(id);
        if (s is null) return null;
        return new SensorDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Tipo = s.Tipo.ToString(),
            Unidade = s.Unidade,
            LimiteMin = s.LimiteMin,
            LimiteMax = s.LimiteMax,
            SateliteId = s.SateliteId
        };
    }

    public async Task<IEnumerable<SensorDto>> GetBySateliteIdAsync(int sateliteId)
    {
        return await _context.Sensores
            .Where(s => s.SateliteId == sateliteId)
            .Select(s => new SensorDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Tipo = s.Tipo.ToString(),
                Unidade = s.Unidade,
                LimiteMin = s.LimiteMin,
                LimiteMax = s.LimiteMax,
                SateliteId = s.SateliteId
            }).ToListAsync();
    }

    public async Task<SensorDto> CreateAsync(CreateSensorDto dto)
    {
        if (dto.LimiteMin >= dto.LimiteMax)
            throw new ArgumentException("LimiteMin deve ser menor que LimiteMax.");

        var sensor = new Sensor
        {
            Nome = dto.Nome,
            Tipo = Enum.Parse<TipoSensor>(dto.Tipo),
            Unidade = dto.Unidade,
            LimiteMin = dto.LimiteMin,
            LimiteMax = dto.LimiteMax,
            SateliteId = dto.SateliteId
        };
        _context.Sensores.Add(sensor);
        await _context.SaveChangesAsync();
        return new SensorDto
        {
            Id = sensor.Id,
            Nome = sensor.Nome,
            Tipo = sensor.Tipo.ToString(),
            Unidade = sensor.Unidade,
            LimiteMin = sensor.LimiteMin,
            LimiteMax = sensor.LimiteMax,
            SateliteId = sensor.SateliteId
        };
    }

    public async Task<SensorDto?> UpdateAsync(int id, CreateSensorDto dto)
    {
        if (dto.LimiteMin >= dto.LimiteMax)
            throw new ArgumentException("LimiteMin deve ser menor que LimiteMax.");

        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor is null) return null;
        sensor.Nome = dto.Nome;
        sensor.Tipo = Enum.Parse<TipoSensor>(dto.Tipo);
        sensor.Unidade = dto.Unidade;
        sensor.LimiteMin = dto.LimiteMin;
        sensor.LimiteMax = dto.LimiteMax;
        sensor.SateliteId = dto.SateliteId;
        await _context.SaveChangesAsync();
        return new SensorDto
        {
            Id = sensor.Id,
            Nome = sensor.Nome,
            Tipo = sensor.Tipo.ToString(),
            Unidade = sensor.Unidade,
            LimiteMin = sensor.LimiteMin,
            LimiteMax = sensor.LimiteMax,
            SateliteId = sensor.SateliteId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor is null) return false;
        _context.Sensores.Remove(sensor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<SensorEstatisticasDto?> GetEstatisticasAsync(int sensorId)
{
    var sensor = await _context.Sensores.FindAsync(sensorId);
    if (sensor is null) return null;

    var leituras = await _context.Leituras
        .Where(l => l.SensorId == sensorId)
        .ToListAsync();

    if (!leituras.Any())
        return new SensorEstatisticasDto
        {
            SensorId = sensorId,
            NomeSensor = sensor.Nome,
            TotalLeituras = 0
        };

    return new SensorEstatisticasDto
    {
        SensorId = sensorId,
        NomeSensor = sensor.Nome,
        TotalLeituras = leituras.Count,
        ValorMedio = Math.Round(leituras.Average(l => l.Valor), 2),
        ValorMinimo = leituras.Min(l => l.Valor),
        ValorMaximo = leituras.Max(l => l.Valor),
        TotalNormal = leituras.Count(l => l.Status == Domain.Enums.StatusLeitura.Normal),
        TotalAlerta = leituras.Count(l => l.Status == Domain.Enums.StatusLeitura.Alerta),
        TotalCritico = leituras.Count(l => l.Status == Domain.Enums.StatusLeitura.Critico)
    };
}
}