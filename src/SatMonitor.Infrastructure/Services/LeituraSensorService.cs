using Microsoft.EntityFrameworkCore;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;
using SatMonitor.Domain.Entities;
using SatMonitor.Infrastructure.Data;

namespace SatMonitor.Infrastructure.Services;

public class LeituraSensorService : ILeituraSensorService
{
    private readonly AppDbContext _context;

    public LeituraSensorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LeituraSensorDto>> GetAllAsync()
    {
        return await _context.Leituras
            .Select(l => new LeituraSensorDto
            {
                Id = l.Id,
                Valor = l.Valor,
                DataHoraLeitura = l.DataHoraLeitura,
                SensorId = l.SensorId
            }).ToListAsync();
    }

    public async Task<LeituraSensorDto?> GetByIdAsync(int id)
    {
        var l = await _context.Leituras.FindAsync(id);
        if (l is null) return null;
        return new LeituraSensorDto
        {
            Id = l.Id,
            Valor = l.Valor,
            DataHoraLeitura = l.DataHoraLeitura,
            SensorId = l.SensorId
        };
    }

    public async Task<IEnumerable<LeituraSensorDto>> GetBySensorIdAsync(int sensorId)
    {
        return await _context.Leituras
            .Where(l => l.SensorId == sensorId)
            .Select(l => new LeituraSensorDto
            {
                Id = l.Id,
                Valor = l.Valor,
                DataHoraLeitura = l.DataHoraLeitura,
                SensorId = l.SensorId
            }).ToListAsync();
    }

    public async Task<LeituraSensorDto> CreateAsync(CreateLeituraSensorDto dto)
    {
        var leitura = new LeituraSensor
        {
            Valor = dto.Valor,
            DataHoraLeitura = dto.DataHoraLeitura,
            SensorId = dto.SensorId
        };
        _context.Leituras.Add(leitura);
        await _context.SaveChangesAsync();
        return new LeituraSensorDto
        {
            Id = leitura.Id,
            Valor = leitura.Valor,
            DataHoraLeitura = leitura.DataHoraLeitura,
            SensorId = leitura.SensorId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var leitura = await _context.Leituras.FindAsync(id);
        if (leitura is null) return false;
        _context.Leituras.Remove(leitura);
        await _context.SaveChangesAsync();
        return true;
    }
}