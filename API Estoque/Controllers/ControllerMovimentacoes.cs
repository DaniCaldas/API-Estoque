using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Estoque.Controllers;

[Authorize]
public static class ControllerMovimentacoes
{
    public static void EndPointMovimentacoes(this WebApplication app)
    {
        var grupo = app.MapGroup("/Movimentacao").RequireAuthorization();

        grupo.MapPost("/", async ([FromBody] Movimentacoes movimentacoes, [FromServices] Database db ) => {
            if (movimentacoes != null) { 
                db.Movimentacoes.Add(movimentacoes);
                await db.SaveChangesAsync();
                return Results.Created($"/Movimentacao/{movimentacoes.Id}", movimentacoes);
            }
            else
            {
                return Results.BadRequest(400);
            }
        });

        grupo.MapPut("/", async (int id, [FromBody] Movimentacoes movimentacoes, [FromServices] Database db) =>
        {
            var movimentacao = db.Movimentacoes.FirstOrDefault(s => s.Id.Equals(id));
            if (movimentacao != null)
            {
                movimentacao.Tipo = movimentacoes.Tipo;
                movimentacao.Quantidade = movimentacoes.Quantidade;
                movimentacao.Produto_id = movimentacoes.Produto_id;
                await db.SaveChangesAsync();
                return Results.Accepted($"/Movimentacao/{movimentacao.Id}", movimentacao);
            }
            else
            {
                return Results.NotFound(404);
            }
        });

        grupo.MapDelete("/", async (int id, [FromServices] Database db) =>
        {
            var movimentacao = db.Movimentacoes.FirstOrDefault(s => s.Id.Equals(id));
            if (movimentacao != null)
            {
                db.Movimentacoes.Remove(movimentacao);
                await db.SaveChangesAsync();
                return Results.Ok(movimentacao);
            }
            else
            {
                return Results.NotFound("Movimentação não encontrada!");
            }
        });
    }
}
