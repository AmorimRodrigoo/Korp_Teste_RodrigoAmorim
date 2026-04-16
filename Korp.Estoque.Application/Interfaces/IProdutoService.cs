using Korp.Estoque.Application.DTOs;
using Korp.Estoque.Domain.Entities;
namespace Korp.Estoque.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoResponseDTO>> ObterTodosAsync();
        Task<ProdutoResponseDTO> AdicionarAsync(ProdutoCreateDTO dto);
        Task AtualizarSaldoAsync(string codigo, int quantidadeParaBaixar);
    }
}
