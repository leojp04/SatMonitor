using SatMonitor.Domain.Enums;

namespace SatMonitor.Domain.Entities;

public class Sensor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoSensor Tipo { get; set; }
    public string Unidade { get; set; } = string.Empty;

    public int SateliteId { get; set; }
    public Satelite Satelite { get; set; } = null!;

    public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();
}