namespace SatMonitor.Application.DTOs;

public class SensorDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public int SateliteId { get; set; }
}

public class CreateSensorDto
{
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public int SateliteId { get; set; }
}