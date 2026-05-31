using Microsoft.AspNetCore.Mvc;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;

namespace SatMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SensoresController : ControllerBase
{
    private readonly ISensorService _service;

    public SensoresController(ISensorService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SensorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var sensores = await _service.GetAllAsync();
        return Ok(sensores);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SensorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var sensor = await _service.GetByIdAsync(id);
        if (sensor is null) return NotFound();
        return Ok(sensor);
    }

    [HttpGet("satelite/{sateliteId}")]
    [ProducesResponseType(typeof(IEnumerable<SensorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySatelite(int sateliteId)
    {
        var sensores = await _service.GetBySateliteIdAsync(sateliteId);
        return Ok(sensores);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SensorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSensorDto dto)
    {
        var sensor = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, sensor);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SensorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateSensorDto dto)
    {
        var sensor = await _service.UpdateAsync(id, dto);
        if (sensor is null) return NotFound();
        return Ok(sensor);
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

    [HttpGet("{id}/estatisticas")]
    [ProducesResponseType(typeof(SensorEstatisticasDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEstatisticas(int id)
    {
        var stats = await _service.GetEstatisticasAsync(id);
        if (stats is null) return NotFound();
        return Ok(stats);
    }
}