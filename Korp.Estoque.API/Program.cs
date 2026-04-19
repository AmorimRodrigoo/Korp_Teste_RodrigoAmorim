using Korp.Estoque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// config banco de dados
var connectionString = builder.Configuration.GetConnectionString("EstoqueConnection");

builder.Services.AddDbContext<EstoqueDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// injeção de dependências
builder.Services.AddScoped<Korp.Estoque.Domain.Interfaces.IProdutoRepository, 
    Korp.Estoque.Infrastructure.Repositories.ProdutoRepository>();
builder.Services.AddScoped<Korp.Estoque.Application.Interfaces.IProdutoService, 
    Korp.Estoque.Application.Services.ProdutoService>();

// cors config
builder.Services.AddCors();
builder.Services.AddControllers();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Korp Estoque API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Gera o JSON do Swagger
    app.UseSwaggerUI(c => // Gera a interface visual (HTML/JS)
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Korp Estoque API v1");
        c.RoutePrefix = string.Empty; // Faz o Swagger abrir direto na raiz (opcional)
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