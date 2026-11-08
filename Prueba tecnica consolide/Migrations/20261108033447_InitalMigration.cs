using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pruebatecnicaconsolide.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id_user);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id_operation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total_amount = table.Column<int>(type: "int", nullable: false),
                    Money_paid = table.Column<int>(type: "int", nullable: false),
                    Money_exchange = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id_operation);
                    table.ForeignKey(
                        name: "FK_Operation_Users_id_user_id",
                        column: x => x.id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id_session = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Session_token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id_session);
                    table.ForeignKey(
                        name: "FK_Session_Users_id_user_id",
                        column: x => x.id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operation_id_user_id",
                table: "Operation",
                column: "id_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Session_id_user_id",
                table: "Session",
                column: "id_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
