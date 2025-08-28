using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendas.API.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "ItensPedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "ItensPedidos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
