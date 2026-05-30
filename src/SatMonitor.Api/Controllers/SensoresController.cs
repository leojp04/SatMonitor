using Microsoft.AspNetCore.Mvc;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;

namespace SatMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensoresController : ControllerBase
{
    private readonly ISensorService _service;

    public SensoresController(ISensorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sensores = await _service.GetAllAsync();
        return Ok(sensores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sensor = await _service.GetByIdAsync(id);
        if (sensor is null) return NotFound();
        return Ok(sensor);
    }

    [HttpGet("satelite/{sateliteId}")]
    public async Task<IActionResult> GetBySatelite(int sateliteId)
    {
        var sensores = await _service.GetBySateliteIdAsync(sateliteId);
        return Ok(sensores);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSensorDto dto)
    {
        var sensor = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, sensor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateSensorDto dto)
    {
        var sensor = await _service.UpdateAsync(id, dto);
        if (sensor is null) return NotFound();
        return Ok(sensor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}