using Microsoft.AspNetCore.Mvc;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;

namespace SatMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissoesController : ControllerBase
{
    private readonly IMissaoService _service;

    public MissoesController(IMissaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var missoes = await _service.GetAllAsync();
        return Ok(missoes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var missao = await _service.GetByIdAsync(id);
        if (missao is null) return NotFound();
        return Ok(missao);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMissaoDto dto)
    {
        var missao = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = missao.Id }, missao);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateMissaoDto dto)
    {
        var missao = await _service.UpdateAsync(id, dto);
        if (missao is null) return NotFound();
        return Ok(missao);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}