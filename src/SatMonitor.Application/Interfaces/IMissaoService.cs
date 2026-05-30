using SatMonitor.Application.DTOs;

namespace SatMonitor.Application.Interfaces;

public interface IMissaoService
{
    Task<IEnumerable<MissaoDto>> GetAllAsync();
    Task<MissaoDto?> GetByIdAsync(int id);
    Task<MissaoDto> CreateAsync(CreateMissaoDto dto);
    Task<MissaoDto?> UpdateAsync(int id, CreateMissaoDto dto);
    Task<bool> DeleteAsync(int id);
}