#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

namespace RedRiftGame.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HostName = table.Column<string>(type: "text", nullable: false),
                    GuestName = table.Column<string>(type: "text", nullable: false),
                    HostFinalHealth = table.Column<int>(type: "integer", nullable: false),
                    GuestFinalHealth = table.Column<int>(type: "integer", nullable: false),
                    TotalTurnsPlayed = table.Column<int>(type: "integer", nullable: false),
                    FinishedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
