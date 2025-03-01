using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectAntivirusBackend.Migrations
{
    /// <inheritdoc />
    public partial class SyncSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_opportunities_opportunity_types_opportunity_type_id",
                table: "opportunities");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "opportunity_types",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "opportunity_types",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "opportunity_types",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "opportunities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_opportunities_opportunity_types_opportunity_type_id",
                table: "opportunities",
                column: "opportunity_type_id",
                principalTable: "opportunity_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_opportunities_opportunity_types_opportunity_type_id",
                table: "opportunities");

            migrationBuilder.DropColumn(
                name: "status",
                table: "opportunities");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "opportunity_types",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "opportunity_types",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "opportunity_types",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_opportunities_opportunity_types_opportunity_type_id",
                table: "opportunities",
                column: "opportunity_type_id",
                principalTable: "opportunity_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
