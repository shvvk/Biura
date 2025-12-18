using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biura.DAL.Migrations
{
    /// <inheritdoc />
    public partial class new2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursion_Operators_TripOperatorId",
                table: "Excursion");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Excursion_TripDetailsId",
                table: "Registries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Excursion",
                table: "Excursion");

            migrationBuilder.RenameTable(
                name: "Excursion",
                newName: "Excursions");

            migrationBuilder.RenameIndex(
                name: "IX_Excursion_TripOperatorId",
                table: "Excursions",
                newName: "IX_Excursions_TripOperatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Excursions",
                table: "Excursions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursions_Operators_TripOperatorId",
                table: "Excursions",
                column: "TripOperatorId",
                principalTable: "Operators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Excursions_TripDetailsId",
                table: "Registries",
                column: "TripDetailsId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excursions_Operators_TripOperatorId",
                table: "Excursions");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Excursions_TripDetailsId",
                table: "Registries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Excursions",
                table: "Excursions");

            migrationBuilder.RenameTable(
                name: "Excursions",
                newName: "Excursion");

            migrationBuilder.RenameIndex(
                name: "IX_Excursions_TripOperatorId",
                table: "Excursion",
                newName: "IX_Excursion_TripOperatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Excursion",
                table: "Excursion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Excursion_Operators_TripOperatorId",
                table: "Excursion",
                column: "TripOperatorId",
                principalTable: "Operators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Excursion_TripDetailsId",
                table: "Registries",
                column: "TripDetailsId",
                principalTable: "Excursion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
