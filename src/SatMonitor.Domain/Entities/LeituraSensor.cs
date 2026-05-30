namespace SatMonitor.Domain.Entities;

public class LeituraSensor
{
    public int Id { get; set; }
    public double Valor { get; set; }
    public DateTime DataHoraLeitura { get; set; }

    public int SensorId { get; set; }
    public Sensor Sensor { get; set; } = null!;
}