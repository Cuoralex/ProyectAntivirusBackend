using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectAntivirusBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddAverageScoreToOpportunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "average_score",
                table: "opportunities",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "average_score",
                table: "opportunities");
        }
    }
}
