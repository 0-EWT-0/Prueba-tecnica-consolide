using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pruebatecnicaconsolide.Migrations
{
    /// <inheritdoc />
    public partial class migration01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Users_Id_user_id",
                table: "Operation");

            migrationBuilder.AlterColumn<int>(
                name: "Id_user_id",
                table: "Operation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Users_Id_user_id",
                table: "Operation",
                column: "Id_user_id",
                principalTable: "Users",
                principalColumn: "Id_user",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Users_Id_user_id",
                table: "Operation");

            migrationBuilder.AlterColumn<int>(
                name: "Id_user_id",
                table: "Operation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Users_Id_user_id",
                table: "Operation",
                column: "Id_user_id",
                principalTable: "Users",
                principalColumn: "Id_user");
        }
    }
}
