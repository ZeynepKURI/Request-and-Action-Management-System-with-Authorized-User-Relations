using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mmmMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actions_requests_RequestId",
                table: "actions");

            migrationBuilder.DropForeignKey(
                name: "FK_requests_users_UserId",
                table: "requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_requests",
                table: "requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_actions",
                table: "actions");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "requests",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "actions",
                newName: "Actions");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_requests_UserId",
                table: "Requests",
                newName: "IX_Requests_UserId");

            migrationBuilder.RenameColumn(
                name: "ActionDescription",
                table: "Actions",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_actions_RequestId",
                table: "Actions",
                newName: "IX_Actions_RequestId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Requests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Requests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "Actions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AssignedTo",
                table: "Actions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Actions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Actions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actions",
                table: "Actions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_AssignedTo",
                table: "Actions",
                column: "AssignedTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Requests_RequestId",
                table: "Actions",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Users_AssignedTo",
                table: "Actions",
                column: "AssignedTo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_UserId",
                table: "Requests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Requests_RequestId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Users_AssignedTo",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_UserId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actions",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_AssignedTo",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Actions");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "requests");

            migrationBuilder.RenameTable(
                name: "Actions",
                newName: "actions");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "Password");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_UserId",
                table: "requests",
                newName: "IX_requests_UserId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "actions",
                newName: "ActionDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_RequestId",
                table: "actions",
                newName: "IX_actions_RequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_requests",
                table: "requests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_actions",
                table: "actions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_actions_requests_RequestId",
                table: "actions",
                column: "RequestId",
                principalTable: "requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_requests_users_UserId",
                table: "requests",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
