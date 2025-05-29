using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Estoque.Migrations
{
    /// <inheritdoc />
    public partial class alterTable_Movimentacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resp_Movimentacao",
                table: "Movimentacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resp_Movimentacao",
                table: "Movimentacoes");
        }
    }
}
