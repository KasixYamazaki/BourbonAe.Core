using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BourbonAe.Core.Migrations
{
    /// <inheritdoc />
    public partial class CreateAEMM0010 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AEKB0040",
                columns: table => new
                {
                    APPLY_NO = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DEPT_CD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    APPLICANT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    APPLY_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEKB0040", x => x.APPLY_NO);
                });

            migrationBuilder.CreateTable(
                name: "AEMM0010",
                columns: table => new
                {
                    CODE = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AEMM0010", x => x.CODE);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AEKB0040");

            migrationBuilder.DropTable(
                name: "AEMM0010");
        }
    }
}
