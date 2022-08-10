using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F.API.Migrations
{
    public partial class Remove_Score_From_Player : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Player");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "Player",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
