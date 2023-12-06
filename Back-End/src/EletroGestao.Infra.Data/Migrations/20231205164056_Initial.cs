using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EletroGestao.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    nomeRazaoSocial = table.Column<string>(type: "varchar(255)", nullable: false),
                    documento = table.Column<string>(type: "varchar(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdCliente = table.Column<Guid>(type: "char(36)", nullable: false),
                    nomeCliente = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdProduto = table.Column<Guid>(type: "char(36)", nullable: false),
                    nomeProduto = table.Column<string>(type: "varchar(255)", nullable: false),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: false),
                    regiao = table.Column<string>(type: "varchar(50)", nullable: false),
                    numeroPedido = table.Column<string>(type: "varchar(50)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime", nullable: false),
                    valorProduto = table.Column<decimal>(type: "decimal(15,5)", nullable: false),
                    valorFrete = table.Column<decimal>(type: "decimal(15,5)", nullable: false),
                    valorFinal = table.Column<decimal>(type: "decimal(15,5)", nullable: false),
                    dataEntrega = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    nomeProduto = table.Column<string>(type: "varchar(255)", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(15,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
