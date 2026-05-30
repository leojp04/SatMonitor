using SatMonitor.Domain.Enums;

namespace SatMonitor.Domain.Entities;

public class Missao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataLancamento { get; set; }
    public StatusMissao Status { get; set; }

    public ICollection<Satelite> Satelites { get; set; } = new List<Satelite>();
}