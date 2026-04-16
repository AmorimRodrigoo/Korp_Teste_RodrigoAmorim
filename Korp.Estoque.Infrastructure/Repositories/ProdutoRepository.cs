using Korp.Estoque.Domain.Entities;
using Korp.Estoque.Domain.Interfaces;
using Korp.Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Korp.Estoque.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly EstoqueDbContext _context;

        public ProdutoRepository(EstoqueDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto?> ObterPorCodigoAsync(string codigo)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<Produto?> ObterPorIdAsync(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Guid id)
        {
            var produto = await ObterPorIdAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
