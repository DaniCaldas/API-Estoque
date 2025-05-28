namespace API_Estoque.Model
{
    public class Movimentacoes
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public int Produto_id { get; set; }
        public DateTime Criado_em { get; set; }
    }
}
