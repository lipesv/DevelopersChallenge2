using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OFX.Data.Migrations
{
    public partial class dbDevelopersChallengev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Statement_Id",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Statement_Id",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Statement");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Statement");

            migrationBuilder.AddColumn<Guid>(
                name: "StatementId",
                table: "Status",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StatementId",
                table: "Account",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Status_StatementId",
                table: "Status",
                column: "StatementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_StatementId",
                table: "Account",
                column: "StatementId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Statement_StatementId",
                table: "Account",
                column: "StatementId",
                principalTable: "Statement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Status_Statement_StatementId",
                table: "Status",
                column: "StatementId",
                principalTable: "Statement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Statement_StatementId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Status_Statement_StatementId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Status_StatementId",
                table: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Account_StatementId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "StatementId",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "StatementId",
                table: "Account");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Statement",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Statement",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

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
    }
}
