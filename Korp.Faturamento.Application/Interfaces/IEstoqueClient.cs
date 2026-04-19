namespace Korp.Faturamento.Application.Interfaces;

public interface IEstoqueClient
{
    Task BaixarSaldoProdutoAsync(string codigo, int quantidade);
}