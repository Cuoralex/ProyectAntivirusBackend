using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectAntivirusBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddOpportunitiesToInstitution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_opportunities_institutions_institution_id",
                table: "opportunities");

            migrationBuilder.AddForeignKey(
                name: "FK_opportunities_institutions_institution_id",
                table: "opportunities",
                column: "institution_id",
                principalTable: "institutions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_opportunities_institutions_institution_id",
                table: "opportunities");

            migrationBuilder.AddForeignKey(
                name: "FK_opportunities_institutions_institution_id",
                table: "opportunities",
                column: "institution_id",
                principalTable: "institutions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
