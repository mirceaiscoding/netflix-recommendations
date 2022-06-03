using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie4U.Migrations
{
    public partial class changePK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Title_Countries_Countries_country_code",
                table: "Title_Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Title_Countries",
                table: "Title_Countries");

            migrationBuilder.DropIndex(
                name: "IX_Title_Countries_country_code",
                table: "Title_Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "country_code",
                table: "Title_Countries");

            migrationBuilder.AddColumn<int>(
                name: "country_id",
                table: "Title_Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "countrycode",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Title_Countries",
                table: "Title_Countries",
                columns: new[] { "netflix_id", "country_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Title_Countries_country_id",
                table: "Title_Countries",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Title_Countries_Countries_country_id",
                table: "Title_Countries",
                column: "country_id",
                principalTable: "Countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Title_Countries_Countries_country_id",
                table: "Title_Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Title_Countries",
                table: "Title_Countries");

            migrationBuilder.DropIndex(
                name: "IX_Title_Countries_country_id",
                table: "Title_Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "Title_Countries");

            migrationBuilder.AddColumn<string>(
                name: "country_code",
                table: "Title_Countries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "countrycode",
                table: "Countries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Title_Countries",
                table: "Title_Countries",
                columns: new[] { "netflix_id", "country_code" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "countrycode");

            migrationBuilder.CreateIndex(
                name: "IX_Title_Countries_country_code",
                table: "Title_Countries",
                column: "country_code");

            migrationBuilder.AddForeignKey(
                name: "FK_Title_Countries_Countries_country_code",
                table: "Title_Countries",
                column: "country_code",
                principalTable: "Countries",
                principalColumn: "countrycode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
