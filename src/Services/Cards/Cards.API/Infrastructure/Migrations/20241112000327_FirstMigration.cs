using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cards.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cards");

            migrationBuilder.CreateTable(
                name: "Card",
                schema: "cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    SecurityNumber = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ExpireDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CardLimit = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 1000m),
                    TotalExpenses = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.CheckConstraint("CK_TotalExpenses_Amount", "TotalExpenses >= 0");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardNumber",
                schema: "cards",
                table: "Card",
                column: "CardNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card",
                schema: "cards");
        }
    }
}
