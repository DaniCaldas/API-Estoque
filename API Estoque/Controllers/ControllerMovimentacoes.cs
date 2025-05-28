using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Estoque.Controllers
{
    public static class ControllerMovimentacoes
    {
        public static void EndPointMovimentacoes(this WebApplication app)
        {
            app.MapPost("/Movimentacao", ([FromBody] Movimentacoes movimentacoes, [FromServices] Database db ) => {
                if (movimentacoes != null) { 
                    var movimentacao = db.Movimentacoes.Add(movimentacoes);
                    db.SaveChanges();
                    Results.Ok(movimentacao);
                }
                else
                {
                    Results.BadRequest(400);
                }
            });
        }
    }
}
