namespace Korp.Faturamento.Application.DTOs;

public class NotaFiscalResponseDTO
{
    public Guid Id { get; set; }
    public int NumeroSequencial { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public List<NotaFiscalItemResponseDTO> Itens { get; set; } = new();
    
    public class NotaFiscalItemResponseDTO
    {
        public string ProdutoCodigo { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
    
}