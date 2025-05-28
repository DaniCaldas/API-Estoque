using API_Estoque.Controllers;
using API_Estoque.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Database>();
builder.Services.AddTransient<Database>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.EndPointProdutos();

app.EndPointCategorias();

app.EndPointMovimentacoes();

app.FiltroPesquisa();

app.FiltroPesquisaData();

app.FiltroProdutos();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
