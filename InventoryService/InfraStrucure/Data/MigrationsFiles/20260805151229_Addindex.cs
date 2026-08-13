using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.InfraStrucure.Data.MigrationsFiles
{
    /// <inheritdoc />
    public partial class Addindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Whouses_Name",
                table: "Whouses");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Whouses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Whouses_Code_Name",
                table: "Whouses",
                columns: new[] { "Code", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Whouses_Code_Name",
                table: "Whouses");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Whouses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Whouses_Name",
                table: "Whouses",
                column: "Name",
                unique: true);
        }
    }
}
