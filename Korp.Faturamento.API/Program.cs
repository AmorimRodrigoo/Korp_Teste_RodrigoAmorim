using Korp.Faturamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// config banco de dados
var connectionString = builder.Configuration.GetConnectionString("FaturamentoConnection");

builder.Services.AddDbContext<FaturamentoDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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

app.MapControllers();

app.Run();
