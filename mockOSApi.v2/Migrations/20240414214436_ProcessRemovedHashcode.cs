using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mockOSApi.v2.Migrations
{
    /// <inheritdoc />
    public partial class ProcessRemovedHashcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashCode",
                table: "MockProcesses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HashCode",
                table: "MockProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
