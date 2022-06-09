using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie4U.Migrations
{
    public partial class FixedPreferenceMisspelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prefference",
                table: "Watcher_Titles",
                newName: "preference");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "preference",
                table: "Watcher_Titles",
                newName: "prefference");
        }
    }
}
