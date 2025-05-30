using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Estoque.Controllers;
public static class ControllerUsuarios
{
    public static void EndPointUsuarios(this WebApplication app)
    {
        app.MapPost("/usuarios", async ([FromBody] Usuarios usuario, [FromServices] Database db) => { 
            if (usuario != null)
            {
                var usuarioExiste = db.Usuarios.FirstOrDefault(s => s.Email.Equals(usuario.Email));
                if(usuarioExiste != null)
                {
                    return Results.Conflict("Usuario ja cadastrado");
                }
                db.Usuarios.Add(usuario);
                await db.SaveChangesAsync();
                return Results.Ok(usuario);
            }
            else {
                return Results.BadRequest();
            }
        });

        app.MapPut("/usuarios", async (int id, [FromBody] Usuarios usuario, [FromServices] Database db) =>
        {
            if (usuario.Senha != null) { 
                var usuarioExiste = db.Usuarios.FirstOrDefault(s => s.Id == id);
                if (usuarioExiste != null) {
                    usuarioExiste.Senha = usuario.Senha;
                    db.Usuarios.Update(usuarioExiste);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }
                else
                {
                    return Results.NotFound();
                }
            }
            else
            {
                return Results.BadRequest();
            }
        });

        app.MapPost("/login",async ([FromBody] Usuarios usuario, [FromServices] Database db, [FromServices] JwtService jwtService) => {
            if (usuario.Email != null && usuario.Senha != null) {
                var usuarioExiste = db.Usuarios.FirstOrDefault(s => s.Email == usuario.Email && s.Senha == usuario.Senha);
                if (usuarioExiste != null) {
                    var token = jwtService.GenerateToken(usuarioExiste);
                    return Results.Ok(token);
                }
                else
                {
                    return Results.BadRequest("usuario nao encontrado!");
                }
            }
            else
            {
                return Results.BadRequest(usuario);
            }
        });
    }
}

