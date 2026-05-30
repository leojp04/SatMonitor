using SatMonitor.Application.DTOs;

namespace SatMonitor.Application.Interfaces;

public interface ILeituraSensorService
{
    Task<IEnumerable<LeituraSensorDto>> GetAllAsync();
    Task<LeituraSensorDto?> GetByIdAsync(int id);
    Task<IEnumerable<LeituraSensorDto>> GetBySensorIdAsync(int sensorId);
    Task<LeituraSensorDto> CreateAsync(CreateLeituraSensorDto dto);
    Task<bool> DeleteAsync(int id);
}