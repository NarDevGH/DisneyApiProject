using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisneyApi.Migrations.DisneyDb
{
    public partial class Fixed_Title_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Media",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Media",
                newName: "Tittle");
        }
    }
}
