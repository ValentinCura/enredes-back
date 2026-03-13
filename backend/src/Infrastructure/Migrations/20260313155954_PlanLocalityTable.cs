using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PlanLocalityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Localities_LocalityId",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_LocalityId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Colors",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "LocalityId",
                table: "Plans");

            migrationBuilder.CreateTable(
                name: "PlanLocalities",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "integer", nullable: false),
                    LocalityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanLocalities", x => new { x.PlanId, x.LocalityId });
                    table.ForeignKey(
                        name: "FK_PlanLocalities_Localities_LocalityId",
                        column: x => x.LocalityId,
                        principalTable: "Localities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanLocalities_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanLocalities_LocalityId",
                table: "PlanLocalities",
                column: "LocalityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanLocalities");

            migrationBuilder.AddColumn<List<string>>(
                name: "Colors",
                table: "Plans",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "Featured",
                table: "Plans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LocalityId",
                table: "Plans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plans_LocalityId",
                table: "Plans",
                column: "LocalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Localities_LocalityId",
                table: "Plans",
                column: "LocalityId",
                principalTable: "Localities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
