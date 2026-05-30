namespace SatMonitor.Application.DTOs;

public class SateliteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public double Altitude { get; set; }
    public double Inclinacao { get; set; }
    public DateTime DataLancamento { get; set; }
    public int MissaoId { get; set; }
}

public class CreateSateliteDto
{
    public string Nome { get; set; } = string.Empty;
    public double Altitude { get; set; }
    public double Inclinacao { get; set; }
    public DateTime DataLancamento { get; set; }
    public int MissaoId { get; set; }
}