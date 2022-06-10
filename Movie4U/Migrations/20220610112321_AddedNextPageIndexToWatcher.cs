using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie4U.Migrations
{
    public partial class AddedNextPageIndexToWatcher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nextPageIndex",
                table: "Watchers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nextPageIndex",
                table: "Watchers");
        }
    }
}
