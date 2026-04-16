using System.Collections.Generic;
using System.Threading.Tasks;
using Korp.Faturamento.Domain.Entities;

namespace Korp.Faturamento.Domain.Interfaces;

public interface INotaFiscalRepository
{
    Task<IEnumerable<NotaFiscal>> ObterTodasAsync();
    Task<NotaFiscal?> ObterPorIdAsync(Guid id);
    Task AdicionarAsync(NotaFiscal notaFiscal);
    Task AtualizarAsync(NotaFiscal notaFiscal);
}