using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Korp.Faturamento.Domain.Entities;

public class NotaFiscalItem
{
    public int Id { get; set; }

    public Guid NotaFiscalId { get; set; }

    [JsonIgnore]
    public NotaFiscal? NotaFiscal { get; set; } // O "?" resolve o aviso

    [Required]
    [MaxLength(50)]
    public string ProdutoCodigo { get; set; } = string.Empty;

    [Required]
    public int Quantidade { get; set; }
}