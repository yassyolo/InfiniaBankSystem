using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedDateOfIssue",
                table: "IdentityCards");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 27, 10, 55, 31, 379, DateTimeKind.Local).AddTicks(3943));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 27, 10, 55, 31, 379, DateTimeKind.Local).AddTicks(3943));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 26, 10, 55, 31, 379, DateTimeKind.Local).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 26, 10, 55, 31, 379, DateTimeKind.Local).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 9, 6, 10, 55, 31, 379, DateTimeKind.Local).AddTicks(3806));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 9, 6, 10, 55, 31, 379, DateTimeKind.Local).AddTicks(3806));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87f02bd3-bfb4-4616-9187-07f3122c3772", "AQAAAAEAACcQAAAAEBsJ6LsDiJic0xGA7g3v2FmP819IEwsYWObC80W70Cx2EI1hVQN4vNIXR1oIU2M6+w==", "5832ac9c-f674-4e53-a03e-19468231eb19" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "EncryptedDateOfIssue",
                table: "IdentityCards",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "Identity card date of issue");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 26, 9, 10, 14, 920, DateTimeKind.Local).AddTicks(1964));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 26, 9, 10, 14, 920, DateTimeKind.Local).AddTicks(1964));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 25, 9, 10, 14, 920, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 25, 9, 10, 14, 920, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 9, 5, 9, 10, 14, 920, DateTimeKind.Local).AddTicks(1809));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 9, 5, 9, 10, 14, 920, DateTimeKind.Local).AddTicks(1809));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca85d385-3a2e-4289-982a-8bb9024f7478", "AQAAAAEAACcQAAAAEAI95UCbbYk49vKZzXZnjiRSSCL6QRwaFooMukj94H7Fiys/7OklNl9givz/eayi2Q==", "2daca5d8-c435-4bec-8aec-282e725813d1" });

            migrationBuilder.UpdateData(
                table: "IdentityCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "EncryptedDateOfIssue",
                value: new byte[] { 179, 85, 117, 239, 66, 92, 60, 97, 70, 3, 196, 223, 92, 112, 85, 69, 175, 173, 235, 42, 60, 70, 176, 166, 214, 41, 252, 156, 139, 109, 140, 130 });
        }
    }
}
