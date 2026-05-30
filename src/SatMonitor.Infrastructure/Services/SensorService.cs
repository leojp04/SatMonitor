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
                SateliteId = s.SateliteId
            }).ToListAsync();
    }

    public async Task<SensorDto> CreateAsync(CreateSensorDto dto)
    {
        var sensor = new Sensor
        {
            Nome = dto.Nome,
            Tipo = Enum.Parse<TipoSensor>(dto.Tipo),
            Unidade = dto.Unidade,
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
            SateliteId = sensor.SateliteId
        };
    }

    public async Task<SensorDto?> UpdateAsync(int id, CreateSensorDto dto)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor is null) return null;
        sensor.Nome = dto.Nome;
        sensor.Tipo = Enum.Parse<TipoSensor>(dto.Tipo);
        sensor.Unidade = dto.Unidade;
        sensor.SateliteId = dto.SateliteId;
        await _context.SaveChangesAsync();
        return new SensorDto
        {
            Id = sensor.Id,
            Nome = sensor.Nome,
            Tipo = sensor.Tipo.ToString(),
            Unidade = sensor.Unidade,
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
}