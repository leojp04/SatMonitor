using SatMonitor.Application.DTOs;

namespace SatMonitor.Application.Interfaces;

public interface ISateliteService
{
    Task<IEnumerable<SateliteDto>> GetAllAsync();
    Task<SateliteDto?> GetByIdAsync(int id);
    Task<IEnumerable<SateliteDto>> GetByMissaoIdAsync(int missaoId);
    Task<SateliteDto> CreateAsync(CreateSateliteDto dto);
    Task<SateliteDto?> UpdateAsync(int id, CreateSateliteDto dto);
    Task<bool> DeleteAsync(int id);
}