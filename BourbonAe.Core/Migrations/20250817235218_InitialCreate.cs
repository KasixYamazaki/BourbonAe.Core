using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BourbonAe.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AESJ1110",
                columns: table => new
                {
                    SLIP_NO = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CUSTOMER_CD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CUSTOMER_NM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SLIP_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AESJ1110", x => x.SLIP_NO);
                });

            migrationBuilder.CreateTable(
                name: "M_TOKUI",
                columns: table => new
                {
                    KY_TOKUI_CD = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TOKUI_NM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TOKUI_KANA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_TOKUI", x => x.KY_TOKUI_CD);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AESJ1110");

            migrationBuilder.DropTable(
                name: "M_TOKUI");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
