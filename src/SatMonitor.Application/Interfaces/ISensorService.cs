using SatMonitor.Application.DTOs;

namespace SatMonitor.Application.Interfaces;

public interface ISensorService
{
    Task<IEnumerable<SensorDto>> GetAllAsync();
    Task<SensorDto?> GetByIdAsync(int id);
    Task<IEnumerable<SensorDto>> GetBySateliteIdAsync(int sateliteId);
    Task<SensorDto> CreateAsync(CreateSensorDto dto);
    Task<SensorDto?> UpdateAsync(int id, CreateSensorDto dto);
    Task<bool> DeleteAsync(int id);
    Task<SensorEstatisticasDto?> GetEstatisticasAsync(int sensorId);
}