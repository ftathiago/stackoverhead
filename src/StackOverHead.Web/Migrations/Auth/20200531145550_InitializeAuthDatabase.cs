using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverHead.Web.Migrations.Auth
{
    public partial class InitializeAuthDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Hash = table.Column<byte[]>(nullable: false),
                    Salt = table.Column<byte[]>(nullable: false),
                    Roles = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "Id", "Email", "Hash", "Name", "Roles", "Salt" },
                values: new object[] { new Guid("c2758290-4233-4003-b272-5689d0f5cb65"), "admin@admin.com", new byte[] { 21, 140, 211, 248, 103, 148, 11, 38, 65, 191, 122, 98, 126, 8, 85, 254, 14, 228, 43, 26, 53, 177, 44, 84, 93, 132, 1, 52, 9, 241, 73, 41, 37, 159, 156, 39, 145, 230, 235, 166, 109, 196, 10, 22, 15, 199, 21, 146, 181, 49, 59, 205, 79, 231, 129, 172, 70, 229, 237, 123, 143, 255, 98, 211 }, "admin", "ADMIN", new byte[] { 12, 219, 134, 193, 33, 236, 70, 171, 116, 155, 227, 151, 212, 145, 7, 33, 244, 247, 136, 151, 252, 242, 248, 250, 183, 52, 72, 134, 227, 52, 48, 182, 90, 218, 104, 180, 42, 193, 108, 84, 243, 93, 141, 27, 43, 91, 142, 55, 152, 77, 93, 50, 206, 143, 182, 118, 38, 48, 107, 103, 41, 130, 251, 85, 103, 13, 59, 70, 68, 110, 149, 141, 85, 219, 61, 130, 102, 111, 45, 106, 107, 94, 29, 134, 147, 15, 68, 217, 151, 125, 231, 163, 100, 29, 18, 221, 213, 116, 14, 159, 211, 203, 208, 193, 31, 92, 127, 130, 108, 249, 232, 197, 207, 45, 238, 55, 123, 187, 163, 197, 199, 61, 101, 123, 251, 27, 29, 216 } });

            migrationBuilder.CreateIndex(
                name: "IX_USER_Email",
                table: "USER",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
