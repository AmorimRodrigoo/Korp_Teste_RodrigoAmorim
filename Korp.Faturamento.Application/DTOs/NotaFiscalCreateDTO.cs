using System.ComponentModel.DataAnnotations;

namespace Korp.Faturamento.Application.DTOs;

public class NotaFiscalCreateDTO
{
    [Required]
    [MinLength(1, ErrorMessage = "A nota fiscal deve ter pelo menos um item.")]
    public List<NotaFiscalItemCreateDTO> Itens { get; set; } = new();
}