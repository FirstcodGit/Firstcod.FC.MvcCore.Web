using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Firstcod.FC.MvcCore.Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(nullable: true),
                    TransactionHash = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    StateId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTransactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTransactions_TransactionHash",
                table: "AppTransactions",
                column: "TransactionHash",
                unique: true,
                filter: "[TransactionHash] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTransactions");
        }
    }
}
