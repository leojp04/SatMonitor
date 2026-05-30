using Microsoft.EntityFrameworkCore;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;
using SatMonitor.Domain.Entities;
using SatMonitor.Domain.Enums;
using SatMonitor.Infrastructure.Data;

namespace SatMonitor.Infrastructure.Services;

public class MissaoService : IMissaoService
{
    private readonly AppDbContext _context;

    public MissaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MissaoDto>> GetAllAsync()
    {
        return await _context.Missoes
            .Select(m => new MissaoDto
            {
                Id = m.Id,
                Nome = m.Nome,
                Descricao = m.Descricao,
                DataLancamento = m.DataLancamento,
                Status = m.Status.ToString()
            }).ToListAsync();
    }

    public async Task<MissaoDto?> GetByIdAsync(int id)
    {
        var m = await _context.Missoes.FindAsync(id);
        if (m is null) return null;
        return new MissaoDto
        {
            Id = m.Id,
            Nome = m.Nome,
            Descricao = m.Descricao,
            DataLancamento = m.DataLancamento,
            Status = m.Status.ToString()
        };
    }

    public async Task<MissaoDto> CreateAsync(CreateMissaoDto dto)
    {
        var missao = new Missao
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            DataLancamento = dto.DataLancamento,
            Status = Enum.Parse<StatusMissao>(dto.Status)
        };
        _context.Missoes.Add(missao);
        await _context.SaveChangesAsync();
        return new MissaoDto
        {
            Id = missao.Id,
            Nome = missao.Nome,
            Descricao = missao.Descricao,
            DataLancamento = missao.DataLancamento,
            Status = missao.Status.ToString()
        };
    }

    public async Task<MissaoDto?> UpdateAsync(int id, CreateMissaoDto dto)
    {
        var missao = await _context.Missoes.FindAsync(id);
        if (missao is null) return null;
        missao.Nome = dto.Nome;
        missao.Descricao = dto.Descricao;
        missao.DataLancamento = dto.DataLancamento;
        missao.Status = Enum.Parse<StatusMissao>(dto.Status);
        await _context.SaveChangesAsync();
        return new MissaoDto
        {
            Id = missao.Id,
            Nome = missao.Nome,
            Descricao = missao.Descricao,
            DataLancamento = missao.DataLancamento,
            Status = missao.Status.ToString()
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var missao = await _context.Missoes.FindAsync(id);
        if (missao is null) return false;
        _context.Missoes.Remove(missao);
        await _context.SaveChangesAsync();
        return true;
    }
}