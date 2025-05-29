using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Estoque.Migrations
{
    /// <inheritdoc />
    public partial class alterColumn_responsavel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Resp_Movimentacao",
                table: "Movimentacoes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Resp_Movimentacao",
                table: "Movimentacoes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
