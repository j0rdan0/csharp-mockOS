using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mockOSApi.v2.Migrations
{
    /// <inheritdoc />
    public partial class ProcessAddedFds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MockProcesses");

            migrationBuilder.AddColumn<string>(
                name: "FileDescriptors",
                table: "MockProcesses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileDescriptors",
                table: "MockProcesses");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MockProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
