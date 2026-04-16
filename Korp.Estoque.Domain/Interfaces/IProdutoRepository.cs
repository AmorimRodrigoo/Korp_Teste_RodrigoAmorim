using Korp.Estoque.Domain.Entities;

namespace Korp.Estoque.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto?> ObterPorCodigoAsync(string codigo);
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task DeletarAsync(Guid id);
        Task<Produto?> ObterPorIdAsync(Guid id);
    }
}
