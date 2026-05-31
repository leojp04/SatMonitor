using SatMonitor.Domain.Enums;

namespace SatMonitor.Domain.Entities;

public class Sensor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoSensor Tipo { get; set; }
    public string Unidade { get; set; } = string.Empty;
    public double LimiteMin { get; set; }
    public double LimiteMax { get; set; }

    public int SateliteId { get; set; }
    public Satelite Satelite { get; set; } = null!;

    public ICollection<LeituraSensor> Leituras { get; set; } = new List<LeituraSensor>();

    public StatusLeitura CalcularStatus(double valor)
    {
        if (valor < LimiteMin || valor > LimiteMax)
            return StatusLeitura.Critico;
        
        var faixa = LimiteMax - LimiteMin;
        var margemAlerta = faixa * 0.1;

        if (valor < LimiteMin + margemAlerta || valor > LimiteMax - margemAlerta)
            return StatusLeitura.Alerta;

        return StatusLeitura.Normal;
    }
}