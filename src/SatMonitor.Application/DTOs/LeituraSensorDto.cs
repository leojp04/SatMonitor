namespace SatMonitor.Application.DTOs;

public class LeituraSensorDto
{
    public int Id { get; set; }
    public double Valor { get; set; }
    public DateTime DataHoraLeitura { get; set; }
    public int SensorId { get; set; }
}

public class CreateLeituraSensorDto
{
    public double Valor { get; set; }
    public DateTime DataHoraLeitura { get; set; }
    public int SensorId { get; set; }
}