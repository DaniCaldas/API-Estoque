using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Estoque.Controllers
{
    public static class ControllerCategorias
    {
        public static void EndPointCategorias(this WebApplication app)
        {
            app.MapPost("/Categorias", async ([FromBody] Categorias categoria, [FromServices] Database db) =>
            {
                if (categoria != null) { 
                    var novaCategoria = db.Add(categoria);
                    await db.SaveChangesAsync();
                    Results.Ok(novaCategoria);
                }
                else
                {
                    Results.BadRequest(400);
                }
            });

            app.MapPut("/Categoria/", (string categoria, [FromBody] string novacategoria, [FromServices] Database db) => { 
                if(categoria != null)
                {
                    var categoriaUp = db.Categorias.FirstOrDefault(s =>s.Nome == categoria);
                    if (categoriaUp != null) {
                        categoriaUp.Nome = novacategoria;
                        db.Update(categoriaUp);
                        db.SaveChanges();   
                        Results.Ok(categoriaUp);
                    }
                    else
                    {
                        Results.BadRequest(400);
                    }
                }
            });

            app.MapGet("/Categorias", ([FromServices] Database db) =>
            {
                var categorias = db.Categorias.OrderBy(s => s.Nome).ToList();
                Results.Ok(categorias);
                return categorias;
            });
        }
    }
}
