using Microsoft.AspNetCore.Mvc;
using API_Estoque.Data;
using API_Estoque.Model;
using Microsoft.AspNetCore.Authorization;
namespace API_Estoque.Controllers;

[Authorize]
public static class ControllerFiltro
{
    class DataMovimentacao
    {
        public DateOnly data_unica { get; set; }
        public DateOnly data_inicio { get; set; }
        public DateOnly data_fim { get; set; }
    }

    class ProdutoExtra
    {
        public int quantidade_min { get; set; }
        public int quantidade_max { get; set; }
        public int preco_min { get; set; }
        public int preco_max { get; set; }
        public DateOnly data_unica { get; set; }
        public DateOnly data_inicio { get; set; }
        public DateOnly data_fim { get; set; }
    }
    public static void FiltroPesquisa(this WebApplication app) {
        
        var grupo = app.MapGroup("/FiltroMovimentacao").RequireAuthorization();

        grupo.MapPost("/", (string Filtro, [FromBody] Movimentacoes movimentacao, [FromServices] Database db) =>
        {
            List<Movimentacoes> retorno = null;
            switch (Filtro)
            {
                case "todos":
                    retorno = db.Movimentacoes.Order().ToList();
                    break;
                case "tipo":
                    retorno = db.Movimentacoes.Where(s => s.Tipo == movimentacao.Tipo).ToList();
                    break;
                case "quantidade":
                    retorno = db.Movimentacoes.Where(s => s.Quantidade == movimentacao.Quantidade).ToList();
                    break;
                case "produto":
                    retorno = db.Movimentacoes.Where(s => s.Produto_id == movimentacao.Produto_id).ToList();
                    break;
                case "responsavel":
                    retorno = db.Movimentacoes.Where(s => s.Resp_Movimentacao == movimentacao.Resp_Movimentacao).ToList();
                    break;
            }
            return retorno.ToList();
        });
    }

    public static void FiltroPesquisaData(this WebApplication app)
    {
        var grupo = app.MapGroup("/FiltroMovimentacaoData").RequireAuthorization();

        grupo.MapPost("/", (string Filtro, [FromBody] DataMovimentacao data, [FromServices] Database db) =>
        {
            List<Movimentacoes> movimentacoes = null;
            switch (Filtro)
            {
                case "data":
                    movimentacoes = db.Movimentacoes.Where(s => DateOnly.FromDateTime(s.Criado_em.Date) == data.data_unica).ToList();
                    break;
                case "data_in":
                    movimentacoes = db.Movimentacoes.Where(s => DateOnly.FromDateTime(s.Criado_em.Date) >= data.data_inicio && DateOnly.FromDateTime(s.Criado_em.Date) <= data.data_fim).ToList();
                    break;
            }
            return movimentacoes.ToList();
        });
    }

    public static void FiltroProdutos(this WebApplication app) {

        var grupo = app.MapGroup("/FiltroProdutos").RequireAuthorization();

        grupo.MapPost("/", (string Filtro, [FromBody] Produtos produto, [FromServices] Database db) => {
            List<Produtos> retorno = null;

            switch (Filtro)
            {
                case "todos":
                        retorno = db.Produtos.Order().ToList();
                    break;
                case "nome":
                    retorno = db.Produtos.Where(s => s.Nome == produto.Nome).ToList();
                    break;
                case "quantidade":
                    retorno = db.Produtos.Where(s => s.Quantidade == produto.Quantidade).ToList();
                    break;
                case "categoria":
                    retorno = db.Produtos.Where(s => s.CategoriaId == produto.CategoriaId).ToList();
                    break;
                case "preco":
                    retorno = db.Produtos.Where(s => s.Preco == produto.Preco).ToList();
                    break;
                case "descricao":
                    retorno = db.Produtos.Where(s => s.Descricao == produto.Descricao).ToList();
                    break;
                case "data":
                    retorno = db.Produtos.Where(s => s.Criado_em.Date == produto.Criado_em.Date).ToList();
                    break;
            }
            return retorno.ToList();
        });

        grupo.MapPost("/Extra", (string Filtro, ProdutoExtra produto, Database db) => { 
            List<Produtos> retorno = null;

            switch (Filtro)
            {
                case "quantidade":
                    retorno = db.Produtos.Where(s => s.Quantidade >= produto.quantidade_min && s.Quantidade <= produto.quantidade_max).ToList();
                    break;
                case "preco":
                    retorno = db.Produtos.Where(s => s.Preco >= produto.preco_min && s.Preco <= produto.preco_max).ToList();
                    break;
                case "data":
                    retorno = db.Produtos.Where(s => DateOnly.FromDateTime(s.Criado_em.Date) >= produto.data_inicio && DateOnly.FromDateTime(s.Criado_em.Date) <= produto.data_fim).ToList();
                    break;
            }
            return retorno.ToList();
        });
    }
}