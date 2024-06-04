using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mockOSApi.v2.Migrations
{
    /// <inheritdoc />
    public partial class AddedMockThreadStack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stack",
                table: "MockThreads");

            migrationBuilder.AddColumn<int>(
                name: "StackId",
                table: "MockThreads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MockThreadStacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Stack = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Handle = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockThreadStacks", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MockThreads_StackId",
                table: "MockThreads",
                column: "StackId");

            migrationBuilder.AddForeignKey(
                name: "FK_MockThreads_MockThreadStacks_StackId",
                table: "MockThreads",
                column: "StackId",
                principalTable: "MockThreadStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MockThreads_MockThreadStacks_StackId",
                table: "MockThreads");

            migrationBuilder.DropTable(
                name: "MockThreadStacks");

            migrationBuilder.DropIndex(
                name: "IX_MockThreads_StackId",
                table: "MockThreads");

            migrationBuilder.DropColumn(
                name: "StackId",
                table: "MockThreads");

            migrationBuilder.AddColumn<string>(
                name: "Stack",
                table: "MockThreads",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
