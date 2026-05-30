using Microsoft.AspNetCore.Mvc;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;

namespace SatMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeiturasController : ControllerBase
{
    private readonly ILeituraSensorService _service;

    public LeiturasController(ILeituraSensorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var leituras = await _service.GetAllAsync();
        return Ok(leituras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var leitura = await _service.GetByIdAsync(id);
        if (leitura is null) return NotFound();
        return Ok(leitura);
    }

    [HttpGet("sensor/{sensorId}")]
    public async Task<IActionResult> GetBySensor(int sensorId)
    {
        var leituras = await _service.GetBySensorIdAsync(sensorId);
        return Ok(leituras);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeituraSensorDto dto)
    {
        var leitura = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = leitura.Id }, leitura);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}