using Korp.Faturamento.Domain.Entities;
using Korp.Faturamento.Domain.Interfaces;
using Korp.Faturamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Korp.Faturamento.Infrastructure.Repositories;

public class NotaFiscalRepository :  INotaFiscalRepository
{
    private readonly FaturamentoDbContext _context;

    public NotaFiscalRepository(FaturamentoDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<NotaFiscal>> ObterTodasAsync()
    {
        // O .Include é vital aqui para trazer os itens junto com a nota!
        return await _context.NotasFiscais
            .Include(n => n.Itens)
            .OrderByDescending(n => n.NumeroSequencial)
            .ToListAsync();
    }
    
    public async Task<NotaFiscal?> ObterPorIdAsync(Guid id)
    {
        return await _context.NotasFiscais
            .Include(n => n.Itens)
            .FirstOrDefaultAsync(n => n.Id == id);
    }
    
    public async Task AdicionarAsync(NotaFiscal notaFiscal)
    {
        await _context.NotasFiscais.AddAsync(notaFiscal);
        await _context.SaveChangesAsync();
    }
    
    public async Task AtualizarAsync(NotaFiscal notaFiscal)
    {
        _context.NotasFiscais.Update(notaFiscal);
        await _context.SaveChangesAsync();
    }
}