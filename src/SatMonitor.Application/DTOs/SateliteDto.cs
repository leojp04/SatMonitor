using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Range(160, 36000, ErrorMessage = "Altitude deve estar entre 160 e 36000 km")]
    public double Altitude { get; set; }

    [Range(0, 180, ErrorMessage = "Inclinação deve estar entre 0 e 180 graus")]
    public double Inclinacao { get; set; }

    [Required(ErrorMessage = "Data de lançamento é obrigatória")]
    public DateTime DataLancamento { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "MissaoId inválido")]
    public int MissaoId { get; set; }
}