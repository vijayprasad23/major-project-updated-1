using Microsoft.EntityFrameworkCore.Migrations;

namespace major_project.Data.Migrations
{
    public partial class addnamecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "availability",
                table: "attires",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "attires",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "attires");

            migrationBuilder.AlterColumn<string>(
                name: "availability",
                table: "attires",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
