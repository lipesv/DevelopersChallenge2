using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OFX.Data.Migrations
{
    public partial class dbDevelopersChallengev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_StatementTransaction_StatementTransactionId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_StatementTransaction_StatementTransactionId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Status_StatementTransactionId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Account_StatementTransactionId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "StatementTransactionId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "StatementTransaction");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "StatementTransaction");

            migrationBuilder.DropColumn(
                name: "StatementTransactionId",
                table: "Account");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "StatementTransaction",
                type: "decimal(13,2)",
                precision: 13,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.CreateTable(
                name: "Statement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UId = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StatusId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statement", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Statement_Id",
                table: "Account",
                column: "Id",
                principalTable: "Statement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Statement_Id",
                table: "Status",
                column: "Id",
                principalTable: "Statement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Statement_Id",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Statement_Id",
                table: "Status");

            migrationBuilder.DropTable(
                name: "Statement");

            migrationBuilder.AddColumn<Guid>(
                name: "StatementTransactionId",
                table: "Status",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "StatementTransaction",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13,2)",
                oldPrecision: 13,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "StatementTransaction",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "UId",
                table: "StatementTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "StatementTransactionId",
                table: "Account",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Status_StatementTransactionId",
                table: "Status",
                column: "StatementTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_StatementTransactionId",
                table: "Account",
                column: "StatementTransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_StatementTransaction_StatementTransactionId",
                table: "Account",
                column: "StatementTransactionId",
                principalTable: "StatementTransaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_StatementTransaction_StatementTransactionId",
                table: "Status",
                column: "StatementTransactionId",
                principalTable: "StatementTransaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
