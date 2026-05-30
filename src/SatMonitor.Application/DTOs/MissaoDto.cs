using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Descrição deve ter no máximo 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Data de lançamento é obrigatória")]
    public DateTime DataLancamento { get; set; }

    [Required(ErrorMessage = "Status é obrigatório")]
    [RegularExpression("^(Planejada|Ativa|Encerrada)$", ErrorMessage = "Status deve ser: Planejada, Ativa ou Encerrada")]
    public string Status { get; set; } = string.Empty;
}