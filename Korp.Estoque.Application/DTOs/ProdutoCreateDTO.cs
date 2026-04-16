using System.ComponentModel.DataAnnotations;

namespace Korp.Estoque.Application.DTOs;

public class ProdutoCreateDTO
{
    [Required(ErrorMessage = "O Código é obrigatório")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Descrição é obrigatória")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O Saldo inicial é obrigatório")]
    public int Saldo { get; set; }
}