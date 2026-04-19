using Korp.Faturamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

using Korp.Faturamento.Application.Interfaces;
using Korp.Faturamento.Application.Services;
using Korp.Faturamento.Domain.Interfaces;
using Korp.Faturamento.Infrastructure.Repositories;
using Korp.Faturamento.Infrastructure.HttpClients;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// config banco de dados
var connectionString = builder.Configuration.GetConnectionString("FaturamentoConnection");

builder.Services.AddDbContext<FaturamentoDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//polly
//Injeção de Repositórios e Serviços
builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
builder.Services.AddScoped<INotaFiscalService, NotaFiscalService>();

// configuração polly 
builder.Services.AddHttpClient<IEstoqueClient, EstoqueClient>(client =>
    {
        
        client.BaseAddress = new Uri("http://localhost:5111"); 
    })
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError() // Captura erros 50x (caiu o servidor) ou 408 (timeout)
        .WaitAndRetryAsync(3, retryAttempt => 
            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))); // Tenta 3x com intervalos de 2s, 4s, 8s.

// cors config
builder.Services.AddCors();
builder.Services.AddControllers();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Korp Faturamento API",
        Version = "v1",
        Description = "API para gestão de Notas Fiscais e Itens de Faturamento"
    });
});

var app = builder.Build();

// 4. Configuração do Pipeline de Requisição (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Korp Faturamento API v1");
        c.RoutePrefix = string.Empty; // Swagger abre na raiz (http://localhost:porta/)
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapControllers();
app.Run();
