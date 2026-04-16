using System.ComponentModel.DataAnnotations;

namespace Korp.Faturamento.Application.DTOs;

public class NotaFiscalItemCreateDTO
{
    [Required]
    public string ProdutoCodigo { get; set; } = string.Empty;
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}