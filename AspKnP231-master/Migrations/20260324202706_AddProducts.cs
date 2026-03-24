using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspKnP231.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ShopSections",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ShopProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopSectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopProducts_ShopSections_ShopSectionId",
                        column: x => x.ShopSectionId,
                        principalTable: "ShopSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopSections_Slug",
                table: "ShopSections",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopProducts_ShopSectionId",
                table: "ShopProducts",
                column: "ShopSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProducts_Slug",
                table: "ShopProducts",
                column: "Slug",
                unique: true,
                filter: "[Slug] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShopSections_Slug",
                table: "ShopSections");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "ShopSections",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
