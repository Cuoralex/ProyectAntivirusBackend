using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectAntivirusBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixUserFavoriteRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_favorites_user_id",
                table: "favorites");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "rating_id",
                table: "opportunities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_favorites_user_id",
                table: "favorites",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_favorites_user_id",
                table: "favorites");

            migrationBuilder.DropColumn(
                name: "rating_id",
                table: "opportunities");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_favorites_user_id",
                table: "favorites",
                column: "user_id",
                unique: true);
        }
    }
}
