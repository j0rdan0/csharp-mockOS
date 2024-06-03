using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mockOSApi.v2.Migrations
{
    /// <inheritdoc />
    public partial class ProcessAddedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MockProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MockProcesses");
        }
    }
}
