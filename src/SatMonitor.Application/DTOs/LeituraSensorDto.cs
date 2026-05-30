using System.ComponentModel.DataAnnotations;

namespace SatMonitor.Application.DTOs;

public class LeituraSensorDto
{
    public int Id { get; set; }
    public double Valor { get; set; }
    public DateTime DataHoraLeitura { get; set; }
    public int SensorId { get; set; }
}

public class CreateLeituraSensorDto
{
    [Required(ErrorMessage = "Valor é obrigatório")]
    public double Valor { get; set; }

    [Required(ErrorMessage = "Data e hora da leitura são obrigatórias")]
    public DateTime DataHoraLeitura { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "SensorId inválido")]
    public int SensorId { get; set; }
}