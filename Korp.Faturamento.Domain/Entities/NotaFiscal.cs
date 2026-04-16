using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Korp.Faturamento.Domain.Entities
{
    public class NotaFiscal
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int NumeroSequencial { get; set; } 

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Aberta"; 

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        // Relacionamento EF Core (Uma NF tem vários itens)
        public List<NotaFiscalItem> Itens { get; set; } = new();
    }
}
