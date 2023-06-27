using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_nota_fiscal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    NotaFiscalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroNf = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<double>(type: "float", nullable: false),
                    DataNf = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CnpjEmissorNf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CnpjDestinatarioNf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.NotaFiscalId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotaFiscal");
        }
    }
}
