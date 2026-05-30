using Microsoft.EntityFrameworkCore;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;
using SatMonitor.Domain.Entities;
using SatMonitor.Infrastructure.Data;

namespace SatMonitor.Infrastructure.Services;

public class SateliteService : ISateliteService
{
    private readonly AppDbContext _context;

    public SateliteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SateliteDto>> GetAllAsync()
    {
        return await _context.Satelites
            .Select(s => new SateliteDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Altitude = s.Altitude,
                Inclinacao = s.Inclinacao,
                DataLancamento = s.DataLancamento,
                MissaoId = s.MissaoId
            }).ToListAsync();
    }

    public async Task<SateliteDto?> GetByIdAsync(int id)
    {
        var s = await _context.Satelites.FindAsync(id);
        if (s is null) return null;
        return new SateliteDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Altitude = s.Altitude,
            Inclinacao = s.Inclinacao,
            DataLancamento = s.DataLancamento,
            MissaoId = s.MissaoId
        };
    }

    public async Task<IEnumerable<SateliteDto>> GetByMissaoIdAsync(int missaoId)
    {
        return await _context.Satelites
            .Where(s => s.MissaoId == missaoId)
            .Select(s => new SateliteDto
            {
                Id = s.Id,
                Nome = s.Nome,
                Altitude = s.Altitude,
                Inclinacao = s.Inclinacao,
                DataLancamento = s.DataLancamento,
                MissaoId = s.MissaoId
            }).ToListAsync();
    }

    public async Task<SateliteDto> CreateAsync(CreateSateliteDto dto)
    {
        var satelite = new Satelite
        {
            Nome = dto.Nome,
            Altitude = dto.Altitude,
            Inclinacao = dto.Inclinacao,
            DataLancamento = dto.DataLancamento,
            MissaoId = dto.MissaoId
        };
        _context.Satelites.Add(satelite);
        await _context.SaveChangesAsync();
        return new SateliteDto
        {
            Id = satelite.Id,
            Nome = satelite.Nome,
            Altitude = satelite.Altitude,
            Inclinacao = satelite.Inclinacao,
            DataLancamento = satelite.DataLancamento,
            MissaoId = satelite.MissaoId
        };
    }

    public async Task<SateliteDto?> UpdateAsync(int id, CreateSateliteDto dto)
    {
        var satelite = await _context.Satelites.FindAsync(id);
        if (satelite is null) return null;
        satelite.Nome = dto.Nome;
        satelite.Altitude = dto.Altitude;
        satelite.Inclinacao = dto.Inclinacao;
        satelite.DataLancamento = dto.DataLancamento;
        satelite.MissaoId = dto.MissaoId;
        await _context.SaveChangesAsync();
        return new SateliteDto
        {
            Id = satelite.Id,
            Nome = satelite.Nome,
            Altitude = satelite.Altitude,
            Inclinacao = satelite.Inclinacao,
            DataLancamento = satelite.DataLancamento,
            MissaoId = satelite.MissaoId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var satelite = await _context.Satelites.FindAsync(id);
        if (satelite is null) return false;
        _context.Satelites.Remove(satelite);
        await _context.SaveChangesAsync();
        return true;
    }
}