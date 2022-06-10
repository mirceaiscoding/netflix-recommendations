using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie4U.Migrations
{
    public partial class AddedCountryIdToWatcher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "coutryId",
                table: "Watchers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coutryId",
                table: "Watchers");
        }
    }
}
