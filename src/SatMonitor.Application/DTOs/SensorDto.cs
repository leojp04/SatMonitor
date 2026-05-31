using System.ComponentModel.DataAnnotations;

namespace SatMonitor.Application.DTOs;

public class SensorDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public double LimiteMin { get; set; }
    public double LimiteMax { get; set; }
    public int SateliteId { get; set; }
}

public class CreateSensorDto
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo é obrigatório")]
    [RegularExpression("^(Temperatura|Pressao|Radiacao|Magnetometro)$",
        ErrorMessage = "Tipo deve ser: Temperatura, Pressao, Radiacao ou Magnetometro")]
    public string Tipo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unidade é obrigatória")]
    [MaxLength(20, ErrorMessage = "Unidade deve ter no máximo 20 caracteres")]
    public string Unidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "LimiteMin é obrigatório")]
    public double LimiteMin { get; set; }

    [Required(ErrorMessage = "LimiteMax é obrigatório")]
    public double LimiteMax { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "SateliteId inválido")]
    public int SateliteId { get; set; }
}

public class SensorEstatisticasDto
{
    public int SensorId { get; set; }
    public string NomeSensor { get; set; } = string.Empty;
    public int TotalLeituras { get; set; }
    public double ValorMedio { get; set; }
    public double ValorMinimo { get; set; }
    public double ValorMaximo { get; set; }
    public int TotalNormal { get; set; }
    public int TotalAlerta { get; set; }
    public int TotalCritico { get; set; }
}