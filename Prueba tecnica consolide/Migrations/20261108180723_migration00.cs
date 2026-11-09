using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pruebatecnicaconsolide.Migrations
{
    /// <inheritdoc />
    public partial class migration00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_Users_id_user_id",
                table: "Operation");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Users_id_user_id",
                table: "Session");

            migrationBuilder.RenameColumn(
                name: "id_user_id",
                table: "Session",
                newName: "Id_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Session_id_user_id",
                table: "Session",
                newName: "IX_Session_Id_user_id");

            migrationBuilder.RenameColumn(
                name: "id_user_id",
                table: "Operation",
                newName: "Id_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Operation_id_user_id",
                table: "Operation",
                newName: "IX_Operation_Id_user_id");

            migrationBuilder.AlterColumn<int>(
                name: "Id_user_id",
                table: "Session",
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
                principalColumn: "Id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Users_Id_user_id",
                table: "Session",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Users_Id_user_id",
                table: "Session");

            migrationBuilder.RenameColumn(
                name: "Id_user_id",
                table: "Session",
                newName: "id_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Session_Id_user_id",
                table: "Session",
                newName: "IX_Session_id_user_id");

            migrationBuilder.RenameColumn(
                name: "Id_user_id",
                table: "Operation",
                newName: "id_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Operation_Id_user_id",
                table: "Operation",
                newName: "IX_Operation_id_user_id");

            migrationBuilder.AlterColumn<int>(
                name: "id_user_id",
                table: "Session",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_Users_id_user_id",
                table: "Operation",
                column: "id_user_id",
                principalTable: "Users",
                principalColumn: "Id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Users_id_user_id",
                table: "Session",
                column: "id_user_id",
                principalTable: "Users",
                principalColumn: "Id_user");
        }
    }
}
