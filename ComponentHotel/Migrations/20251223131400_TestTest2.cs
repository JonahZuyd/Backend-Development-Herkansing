using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentHotel.Migrations
{
    /// <inheritdoc />
    public partial class TestTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarieven_Reserveringen_ReserveringId",
                table: "Tarieven");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarieven_Reserveringen_ReserveringId",
                table: "Tarieven",
                column: "ReserveringId",
                principalTable: "Reserveringen",
                principalColumn: "ReserveringId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarieven_Reserveringen_ReserveringId",
                table: "Tarieven");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarieven_Reserveringen_ReserveringId",
                table: "Tarieven",
                column: "ReserveringId",
                principalTable: "Reserveringen",
                principalColumn: "ReserveringId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
