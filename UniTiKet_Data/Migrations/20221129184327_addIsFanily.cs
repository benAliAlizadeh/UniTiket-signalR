using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniTiKet_Data.Migrations
{
    public partial class addIsFanily : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinaly",
                table: "Tikets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinaly",
                table: "Tikets");
        }
    }
}
