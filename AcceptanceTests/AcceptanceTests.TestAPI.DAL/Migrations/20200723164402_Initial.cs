using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcceptanceTests.TestAPI.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    Application = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Allocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    ExpiresAt = table.Column<DateTime>(nullable: true),
                    Allocated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allocation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allocation_UserId",
                table: "Allocation",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_ContactEmail",
                table: "User",
                column: "ContactEmail",
                unique: true,
                filter: "[ContactEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allocation");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
