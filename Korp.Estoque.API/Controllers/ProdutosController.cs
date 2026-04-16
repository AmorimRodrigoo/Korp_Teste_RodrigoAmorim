using Korp.Estoque.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Korp.Estoque.Application.Interfaces;
using Korp.Estoque.Domain.Entities;

namespace Korp.Estoque.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        return Ok(produtos);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProdutoCreateDTO dto)
    {
        try
        {
            var produtoCriado = await _produtoService.AdicionarAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = produtoCriado.Id }, produtoCriado);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
    
    [HttpPut("baixar-saldo")]
    public async Task<IActionResult> BaixarSaldo([FromBody] BaixaSaldoRequestDTO request)
    {
        try
        {
            await _produtoService.AtualizarSaldoAsync(request.Codigo, request.Quantidade);
            return Ok(new { mensagem = "Saldo atualizado com sucesso." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
