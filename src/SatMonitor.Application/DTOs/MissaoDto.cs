namespace SatMonitor.Application.DTOs;

public class MissaoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataLancamento { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateMissaoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataLancamento { get; set; }
    public string Status { get; set; } = string.Empty;
}