using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Estoque.Controllers;

[Authorize]
public static class ControllerCategorias
{
    public static void EndPointCategorias(this WebApplication app)
    {
        var grupo = app.MapGroup("/Categorias").RequireAuthorization();

        grupo.MapPost("/", async ([FromBody] Categorias categoria, [FromServices] Database db) =>
        {
            if (categoria != null) { 
                db.Add(categoria);
                await db.SaveChangesAsync();
                return Results.Created("/Categoria", categoria);
            }
            else
            {
                return Results.BadRequest(400);
            }
        });

        grupo.MapPut("/", async (string id, [FromBody] Categorias categoria, [FromServices] Database db) => {
            var categoriaExistente = db.Categorias.FirstOrDefault(s => s.Id.Equals(id));
            if (categoria != null) { 
                if(categoriaExistente != null)
                {
                    categoriaExistente.Nome = categoria.Nome;
                    await db.SaveChangesAsync();
                    return Results.Accepted($"/Categoria/{categoriaExistente.Id}", categoriaExistente);
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

        grupo.MapGet("/", ([FromServices] Database db) =>
        {
            var categorias = db.Categorias.OrderBy(s => s.Nome).ToList();
            Results.Ok(categorias);
            return categorias;
        });

        grupo.MapDelete("/", async (int id, [FromServices] Database db) =>
        {
            var categoria = db.Categorias.FirstOrDefault(s => s.Id.Equals(id));
            if(categoria == null)
            {
                return Results.NotFound("Categoria não econtrada");
            }
            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();
            return Results.Ok(categoria);
        });
    }
}
