using Microsoft.AspNetCore.Mvc;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;

namespace SatMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LeiturasController : ControllerBase
{
    private readonly ILeituraSensorService _service;

    public LeiturasController(ILeituraSensorService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LeituraSensorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var leituras = await _service.GetAllAsync();
        return Ok(leituras);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeituraSensorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var leitura = await _service.GetByIdAsync(id);
        if (leitura is null) return NotFound();
        return Ok(leitura);
    }

    [HttpGet("sensor/{sensorId}")]
    [ProducesResponseType(typeof(IEnumerable<LeituraSensorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySensor(int sensorId)
    {
        var leituras = await _service.GetBySensorIdAsync(sensorId);
        return Ok(leituras);
    }

    [HttpGet("status/{status}")]
    [ProducesResponseType(typeof(IEnumerable<LeituraSensorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByStatus(string status)
    {
        var validos = new[] { "Normal", "Alerta", "Critico" };
        if (!validos.Contains(status))
            return BadRequest(new { erro = "Status inválido. Use: Normal, Alerta ou Critico" });

        var leituras = await _service.GetByStatusAsync(status);
        return Ok(leituras);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LeituraSensorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateLeituraSensorDto dto)
    {
        var leitura = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = leitura.Id }, leitura);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}