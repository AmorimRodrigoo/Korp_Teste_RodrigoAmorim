using Korp.Faturamento.Application.DTOs;
using Korp.Faturamento.Application.Interfaces;
using Korp.Faturamento.Domain.Entities;
using Korp.Faturamento.Domain.Interfaces;

namespace Korp.Faturamento.Application.Services;

public class NotaFiscalService : INotaFiscalService
{
    private readonly INotaFiscalRepository _repository;
    private readonly IEstoqueClient _estoqueClient;
    
    public NotaFiscalService(INotaFiscalRepository repository, IEstoqueClient estoqueClient)
    {
        _repository = repository;
        _estoqueClient = estoqueClient;
    }
    
    public async Task<IEnumerable<NotaFiscalResponseDTO>> ObterTodasAsync()
    {
        var notas = await _repository.ObterTodasAsync();

        return notas.Select(n => new NotaFiscalResponseDTO
        {
            Id = n.Id,
            NumeroSequencial = n.NumeroSequencial,
            Status = n.Status,
            DataCriacao = n.DataCriacao,
            Itens = n.Itens.Select(i => new NotaFiscalResponseDTO.NotaFiscalItemResponseDTO()
            {
                ProdutoCodigo = i.ProdutoCodigo,
                Quantidade = i.Quantidade
            }).ToList()
        });
    }
    
    public async Task<NotaFiscalResponseDTO> CriarAsync(NotaFiscalCreateDTO dto)
    {
        var notaFiscal = new NotaFiscal()
        {
            Status = "Aberta", // Toda nota nasce aberta
            DataCriacao = DateTime.UtcNow,
            Itens = dto.Itens.Select(i => new NotaFiscalItem
            {
                ProdutoCodigo = i.ProdutoCodigo,
                Quantidade = i.Quantidade
            }).ToList()
        };

        await _repository.AdicionarAsync(notaFiscal);

        return new NotaFiscalResponseDTO
        {
            Id = notaFiscal.Id,
            NumeroSequencial = notaFiscal.NumeroSequencial,
            Status = notaFiscal.Status,
            DataCriacao = notaFiscal.DataCriacao
        };
    }
    
    public async Task ImprimirAsync(Guid id)
    {
        var notaFiscal = await _repository.ObterPorIdAsync(id);
        
        if (notaFiscal == null)
            throw new Exception("Nota fiscal não encontrada.");

        if (notaFiscal.Status == "Fechada")
            throw new Exception("Esta nota fiscal já foi impressa e fechada.");

        // Para cada item na nota, chama o microsserviço de Estoque
        foreach (var item in notaFiscal.Itens)
        {
            try
            {
                await _estoqueClient.BaixarSaldoProdutoAsync(item.ProdutoCodigo, item.Quantidade);
            }
            catch (Exception ex)
            {
                // Se der erro (ex: estoque sem saldo ou API fora do ar após as tentativas do Polly), 
                // a nota continua "Aberta" e subimos o erro para a tela.
                throw new Exception($"Erro ao baixar estoque do produto {item.ProdutoCodigo}. A Nota continuará Aberta. Detalhe: {ex.Message}");
            }
        }

        // Se chegou até aqui, todo o estoque foi baixado com sucesso!
        notaFiscal.Status = "Fechada";
        await _repository.AtualizarAsync(notaFiscal);
    }
}