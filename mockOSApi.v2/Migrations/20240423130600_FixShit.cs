using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mockOSApi.v2.Migrations
{
    /// <inheritdoc />
    public partial class FixShit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashCode",
                table: "MockProcesses");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "MockProcesses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "MockProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HashCode",
                table: "MockProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
