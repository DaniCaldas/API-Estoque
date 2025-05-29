using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Estoque.Controllers;

public static class ControllerProdutos
{
    [Authorize]
    public static void EndPointProdutos(this WebApplication app) {

        var grupo = app.MapGroup("/produtos").RequireAuthorization();

        grupo.MapGet("/", ([FromServices] Database data) =>
        {
            var produtos = data.Produtos.OrderBy(s => s.Criado_em).ToList();
            return produtos;
        });

        grupo.MapPost("/", async ([FromBody] Produtos produto, [FromServices] Database db) => {
            if (produto != null) {
                var verifica = db.Produtos.FirstOrDefault(s => s.Nome.Equals(produto.Nome));
                if(verifica != null)
                {
                    return Results.Conflict("Produto ja cadastrado");
                }
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();
                return Results.Created($"/produtos/{produto.Id}", produto);
            }
            else
            {
                return Results.BadRequest(400);
            }
        });

        grupo.MapPut("/", async (int id, [FromBody] Produtos produto, [FromServices] Database db) => {
            if (produto != null) {
                var produtoUp = db.Produtos.FirstOrDefault(s => s.Id.Equals(id));

                if(produtoUp == null)
                {
                    return Results.Conflict("Produto não cadastrado!");
                }
                produtoUp.Nome = produto.Nome;
                produtoUp.Preco = produto.Preco;
                produtoUp.Descricao = produto.Descricao;
                produtoUp.Quantidade = produto.Quantidade;
                produtoUp.CategoriaId = produto.CategoriaId;
                produtoUp.Criado_em = produto.Criado_em;
                
                db.Produtos.Update(produtoUp);
                await db.SaveChangesAsync();
                return Results.Created($"/produtos/{produto.Id}", produtoUp);   
            }
            else
            {
                return Results.BadRequest(400);
            }
        });

        grupo.MapDelete("/", async (int id, [FromServices] Database db) => {
            if (id > 0)
            {
                var produto = db.Produtos.FirstOrDefault(s => s.Id.Equals(id));
                if (produto != null)
                {
                    db.Produtos.Remove(produto);
                    await db.SaveChangesAsync();
                    return Results.Ok(produto);
                }
                else
                {
                    return Results.NotFound(404);
                }
            }
            else
            {
               return Results.BadRequest(400);
            }
        });
    }
}
