using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace RedRiftGame.DataAccess.Migrations
{
    public partial class AddCreatedAtAndIsHostWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHostWinner",
                table: "Matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Instant>(
                name: "StartedAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: NodaTime.Instant.FromUnixTimeTicks(0L));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHostWinner",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "Matches");
        }
    }
}
