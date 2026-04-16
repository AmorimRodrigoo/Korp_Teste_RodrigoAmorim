using Korp.Estoque.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Korp.Estoque.Infrastructure.Data
{
    public class EstoqueDbContext : DbContext
    {
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options){ }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // garante que o código do produto seja único
            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.Codigo)
                .IsUnique();
        }

    }
}
