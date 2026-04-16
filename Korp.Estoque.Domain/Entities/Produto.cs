using System.ComponentModel.DataAnnotations;

namespace Korp.Estoque.Domain.Entities;

public class Produto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public int Saldo { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}