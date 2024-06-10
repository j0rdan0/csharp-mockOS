using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mockOSApi.v2.Migrations
{
    /// <inheritdoc />
    public partial class addedThreadsToProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MockThreads_MockProcesses_ParentPid",
                table: "MockThreads");

            migrationBuilder.RenameColumn(
                name: "ParentPid",
                table: "MockThreads",
                newName: "MockProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_MockThreads_ParentPid",
                table: "MockThreads",
                newName: "IX_MockThreads_MockProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_MockThreads_MockProcesses_MockProcessId",
                table: "MockThreads",
                column: "MockProcessId",
                principalTable: "MockProcesses",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MockThreads_MockProcesses_MockProcessId",
                table: "MockThreads");

            migrationBuilder.RenameColumn(
                name: "MockProcessId",
                table: "MockThreads",
                newName: "ParentPid");

            migrationBuilder.RenameIndex(
                name: "IX_MockThreads_MockProcessId",
                table: "MockThreads",
                newName: "IX_MockThreads_ParentPid");

            migrationBuilder.AddForeignKey(
                name: "FK_MockThreads_MockProcesses_ParentPid",
                table: "MockThreads",
                column: "ParentPid",
                principalTable: "MockProcesses",
                principalColumn: "Pid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
