namespace API_Estoque.Model
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Preco { get; set; }
        public int Quantidade { get; set; }
        public int CategoriaId {  get; set; }
        public DateTime Criado_em {  get; set; }
    }
}
