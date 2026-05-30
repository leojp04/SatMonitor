using Microsoft.AspNetCore.Mvc;
using SatMonitor.Application.DTOs;
using SatMonitor.Application.Interfaces;

namespace SatMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SatelitesController : ControllerBase
{
    private readonly ISateliteService _service;

    public SatelitesController(ISateliteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var satelites = await _service.GetAllAsync();
        return Ok(satelites);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var satelite = await _service.GetByIdAsync(id);
        if (satelite is null) return NotFound();
        return Ok(satelite);
    }

    [HttpGet("missao/{missaoId}")]
    public async Task<IActionResult> GetByMissao(int missaoId)
    {
        var satelites = await _service.GetByMissaoIdAsync(missaoId);
        return Ok(satelites);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSateliteDto dto)
    {
        var satelite = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = satelite.Id }, satelite);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateSateliteDto dto)
    {
        var satelite = await _service.UpdateAsync(id, dto);
        if (satelite is null) return NotFound();
        return Ok(satelite);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}