using Korp.Faturamento.Application.DTOs;
using Korp.Faturamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Korp.Faturamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotasFiscaisController : ControllerBase
{
    private readonly INotaFiscalService _notaFiscalService;

    public NotasFiscaisController(INotaFiscalService notaFiscalService)
    {
        _notaFiscalService = notaFiscalService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var notas = await _notaFiscalService.ObterTodasAsync();
        return Ok(notas);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NotaFiscalCreateDTO dto)
    {
        try
        {
            var notaCriada = await _notaFiscalService.CriarAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = notaCriada.Id }, notaCriada);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
    
    [HttpPut("{id}/imprimir")]
    public async Task<IActionResult> Imprimir(Guid id)
    {
        try
        {
            await _notaFiscalService.ImprimirAsync(id);
            return Ok(new { mensagem = "Nota fiscal impressa e estoque baixado com sucesso! Status alterado para Fechada." });
        }
        catch (Exception ex)
        {
            // Se o Estoque estiver fora do ar ou sem saldo, cai aqui!
            return StatusCode(503, new { mensagem = ex.Message });
        }
    }
}