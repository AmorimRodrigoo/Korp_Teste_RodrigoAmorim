using Korp.Estoque.Application.DTOs;
using Korp.Estoque.Application.Interfaces;
using Korp.Estoque.Domain.Entities;
using Korp.Estoque.Domain.Interfaces;

namespace Korp.Estoque.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ProdutoResponseDTO>> ObterTodosAsync()
        {
            var produtos = await _repository.ObterTodosAsync();
            
            return produtos.Select(p => new ProdutoResponseDTO
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Descricao = p.Descricao,
                Saldo = p.Saldo
            });
        }
        public async Task<ProdutoResponseDTO> AdicionarAsync(ProdutoCreateDTO dto)
        {
            var produtoExistente = await _repository.ObterPorCodigoAsync(dto.Codigo);
            if (produtoExistente != null)
                throw new Exception($"Já existe um produto cadastrado com o código {dto.Codigo}.");

            // Mapeia DTO -> Entidade para salvar no banco
            var produto = new Produto
            {
                Codigo = dto.Codigo,
                Descricao = dto.Descricao,
                Saldo = dto.Saldo
            };

            await _repository.AdicionarAsync(produto);

            // Retorna o ResponseDTO
            return new ProdutoResponseDTO
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                Saldo = produto.Saldo
            };
        }

        public async Task AtualizarSaldoAsync(string codigo, int quantidadeParaBaixar)
        {
            var produto = await _repository.ObterPorCodigoAsync(codigo);
            if (produto == null)
                throw new Exception($"Produto com código {codigo} não encontrado.");

            if (produto.Saldo < quantidadeParaBaixar)
                throw new Exception($"Saldo insuficiente para o produto {codigo}. Saldo atual: {produto.Saldo}");

            produto.Saldo -= quantidadeParaBaixar;
        
            await _repository.AtualizarAsync(produto);
        }
    }
}
