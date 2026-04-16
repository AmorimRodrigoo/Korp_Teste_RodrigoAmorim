using System.ComponentModel.DataAnnotations;

namespace Korp.Estoque.Application.DTOs;

public class BaixaSaldoRequestDTO
{
    [Required]
    public string Codigo { get; set; } = string.Empty;
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }
}