using Korp.Faturamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Korp.Faturamento.Infrastructure.Data
{
    public class FaturamentoDbContext : DbContext
    {
        public FaturamentoDbContext(DbContextOptions<FaturamentoDbContext> options) : base(options) { }

        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<NotaFiscalItem> NotaFiscalItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //NumeroSequencial será auto incrementado
            modelBuilder.Entity<NotaFiscal>()
                .Property(n => n.NumeroSequencial)
                .ValueGeneratedOnAdd();

            // informa que NumeroSequencial é uma chave alternativa
            modelBuilder.Entity<NotaFiscal>()
                .HasAlternateKey(n => n.NumeroSequencial);
        }
    }
}
