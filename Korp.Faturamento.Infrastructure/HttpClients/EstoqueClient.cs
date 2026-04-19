using System.Text;
using System.Text.Json;
using Korp.Faturamento.Application.Interfaces;

namespace Korp.Faturamento.Infrastructure.HttpClients;

public class EstoqueClient : IEstoqueClient
{
    private readonly HttpClient _httpClient;

    public EstoqueClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task BaixarSaldoProdutoAsync(string codigo, int quantidade)
    {
        // Monta o mesmo JSON que usávamos no Swagger do Estoque
        var payload = new { Codigo = codigo, Quantidade = quantidade };
        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        // Faz o PUT para a rota do microsserviço de estoque
        var response = await _httpClient.PutAsync("/api/produtos/baixar-saldo", content);

        // Se o status code não for 200 (OK), ele lança uma exceção. 
        // É ESSA exceção que o Polly vai capturar para tentar de novo!
        response.EnsureSuccessStatusCode(); 
    }
}