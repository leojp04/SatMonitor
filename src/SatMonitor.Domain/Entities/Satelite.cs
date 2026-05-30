namespace SatMonitor.Domain.Entities;

public class Satelite
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public double Altitude { get; set; }
    public double Inclinacao { get; set; }
    public DateTime DataLancamento { get; set; }

    public int MissaoId { get; set; }
    public Missao Missao { get; set; } = null!;

    public ICollection<Sensor> Sensores { get; set; } = new List<Sensor>();
}