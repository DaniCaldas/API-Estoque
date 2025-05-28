using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API_Estoque.Controllers;

public static class ControllerProdutos
{
    public static void EndPointProdutos(this WebApplication app) {

        app.MapGet("/produtos", ([FromServices] Database data) =>
        {
            var produtos = data.Produtos.OrderBy(s => s.Criado_em).ToList();
            return produtos;
        });

        app.MapPost("/produtos", async ([FromBody] Produtos produto, [FromServices] Database db) => {
            if (produto != null) {
                var verifica = db.Produtos.FirstOrDefault(s => s.Nome.Equals(produto.Nome));
                if (verifica == null)
                {
                    var produtoNovo = db.Produtos.Add(produto);
                    await db.SaveChangesAsync();
                    Results.Ok(produtoNovo);
                }
                else
                {
                    Results.NotFound(401);
                }
            }
            else
            {
                Results.BadRequest(400);
            }
        });

        app.MapPut("/produtos/", async (int id, [FromBody] Produtos produto, [FromServices] Database db) => {
            if (produto != null) {
                var produtoUp = db.Produtos.FirstOrDefault(s => s.Id.Equals(id));

                if (produtoUp != null) {
                    produtoUp.Nome = produto.Nome;
                    produtoUp.Preco = produto.Preco;
                    produtoUp.Descricao = produto.Descricao;
                    produtoUp.Quantidade = produto.Quantidade;
                    produtoUp.CategoriaId = produto.CategoriaId;
                    produtoUp.Criado_em = produto.Criado_em;
                }
                db.Produtos.Update(produtoUp);
                await db.SaveChangesAsync();
                Results.Ok(produtoUp);   
            }
            else
            {
                Results.BadRequest(400);
            }
        });
    }
}
