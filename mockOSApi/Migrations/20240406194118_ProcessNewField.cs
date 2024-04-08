using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mockOSApi.Migrations
{
    /// <inheritdoc />
    public partial class ProcessNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessCounter",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessCounter",
                table: "Processes");
        }
    }
}
