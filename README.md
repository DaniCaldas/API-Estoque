# API de Gerenciamento de Estoque (ASP.NET Core)

Este é um projeto de API para gerenciamento de estoque desenvolvido com C#, ASP.NET Core, Entity Framework e SQL Server.

## Tecnologias Utilizadas

- ASP.NET Core 6.0+
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI para documentação
- Autenticação JWT 

## Funcionalidades

- CRUD completo de produtos
- Controle de categorias
- Gerenciamento de movimentações de estoque (entrada/saída)
- Filtros de Movimentações, Categorias e Produtos
- Autenticação e autorização

## Pré-requisitos

- .NET 6.0 SDK ou superior
- SQL Server (local ou Azure SQL)
- Visual Studio 2022 ou VS Code (recomendado)

## Configuração do Projeto

1. Clone o repositório:
```bash
git clone https://github.com/DaniCaldas/API-Estoque.git
cd API-Estoque
```

2. Configure a string de conexão:
Edite o arquivo `appsettings.json` e atualize a connection string:
```json
"ConnectionStrings": {
  "EstoqueConnection": "Server=seu_servidor;Database=EstoqueDB;User Id=seu_usuario;Password=sua_senha;"
}
```

3. Aplique as migrações do Entity Framework:
```bash
dotnet ef database update
```

4. Execute o projeto:
```bash
dotnet run
```

## Estrutura do Projeto

```
API-Estoque/
├── Controllers/       # Controladores da API
├── Models/            # Modelos de dados e DTOs
├── Data/              # Contexto do EF e configurações
├── Migrations/        # Migrações do banco de dados
├── appsettings.json   # Configurações
└── Program.cs         # Configuração inicial
```

## Rotas da API (Exemplos)

### Produtos
- `GET /produtos` - Lista todos os produtos
- `POST /FiltroProdutos/{filtro}` - Obtém um produto específico se baseando do parametro filtro que pode ser (todos, nome, quantidade, categoria, preco, descricao, data).
- `POST /FiltroProdutosExtra/{filtro}` - Obtém um produto específico obtendo valores minimos e maximos e entre duas datas se baseando do parametro filtro que pode ser (quantidade, preco, data).
- `POST /produtos` - Cria um novo produto
- `PUT /produtos/{id}` - Atualiza um produto
- `DELETE /produtos/{id}` - Remove um produto

### Movimentações
- `POST /FiltroMovimentacao/{filtro}` - Lista as movimentações conforme a pesquisa filtrada (todos, tipo, quantidade, produto)
- `POST /FiltroMovimentacaoData/{filtro}` - Lista as movimentações conforme a pesquisa filtrada por datas(data, data_in)

### Categorias
- `POST /categorias` - Cria uma nova categoria
- `GET /categorias` - Lista as Categorias

### Usuario
- `POST /login` - Faz login do usuario com email e senha
- `POST /usuarios` - Cadastra um novo usuario
- `PUT /usuarios` - Atualiza um usuario

## Documentação da API

A API inclui suporte a Swagger/OpenAPI. Acesse a documentação interativa em:

```
https://localhost:5001/swagger
```

## Padrões Adotados

- Repository Pattern
- Injeção de Dependência
- Migrations para controle de schema do banco

## Como Contribuir

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Faça push para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## Contato

Daniel Caldas - [GitHub](https://github.com/DaniCaldas)  
Email: seu-email@exemplo.com
