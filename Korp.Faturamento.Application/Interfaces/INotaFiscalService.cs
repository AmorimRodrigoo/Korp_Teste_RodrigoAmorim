using Korp.Faturamento.Application.DTOs;

namespace Korp.Faturamento.Application.Interfaces;

public interface INotaFiscalService
{
    Task<IEnumerable<NotaFiscalResponseDTO>> ObterTodasAsync();
    Task<NotaFiscalResponseDTO> CriarAsync(NotaFiscalCreateDTO dto);
    Task ImprimirAsync(Guid id);
}