using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class IncludedSSNForCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "EncryptedSSN",
                table: "IdentityCards",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                comment: "Identity card SSN");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 11, 23, 49, 56, 204, DateTimeKind.Local).AddTicks(9739));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Balance", "CreationDate" },
                values: new object[] { 0m, new DateTime(2024, 8, 11, 23, 49, 56, 204, DateTimeKind.Local).AddTicks(9739) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 10, 23, 49, 56, 204, DateTimeKind.Local).AddTicks(9516));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 10, 23, 49, 56, 204, DateTimeKind.Local).AddTicks(9516));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 21, 23, 49, 56, 204, DateTimeKind.Local).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 21, 23, 49, 56, 204, DateTimeKind.Local).AddTicks(9610));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c8d39f8-6f79-429e-a0f4-dcec9b10c726", "AQAAAAEAACcQAAAAEKeEw4dRfETyFX0Dm+0/K8YRvGtsYGkWsPRCPsezlYiZm4AO6upK5/I/YclvHSNuUA==", "a8433a31-10fc-46bd-b3ba-ae2528439067" });

            migrationBuilder.UpdateData(
                table: "IdentityCards",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EncryptedSSN", "EncryptedSex" },
                values: new object[] { new byte[] { 172, 50, 196, 250, 62, 125, 215, 96, 161, 1, 179, 55, 24, 130, 67, 36, 74, 62, 252, 121, 145, 155, 210, 128, 107, 87, 3, 72, 57, 56, 119, 247 }, new byte[] { 233, 27, 160, 30, 61, 187, 236, 11, 178, 98, 13, 55, 198, 108, 31, 34, 127, 147, 107, 103, 173, 182, 81, 243, 24, 43, 202, 53, 95, 182, 23, 9 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedSSN",
                table: "IdentityCards");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 11, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8663));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Balance", "CreationDate" },
                values: new object[] { 1000000m, new DateTime(2024, 8, 11, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8663) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 10, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8464));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 10, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8464));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 21, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8522));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 21, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8522));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e18a347f-0275-4698-9b03-ec4f2338e14e", "AQAAAAEAACcQAAAAED7aT9Cna9YG79QmfDDLYMES027CxYTo6w/JGIQTfB4eG8jHgG50wPU8QmmTBFiKaQ==", "3d8c4a0b-af73-403f-bc60-0e70ddcd543a" });

            migrationBuilder.UpdateData(
                table: "IdentityCards",
                keyColumn: "Id",
                keyValue: 1,
                column: "EncryptedSex",
                value: new byte[] { 139, 175, 173, 247, 127, 244, 248, 147, 1, 211, 148, 110, 2, 137, 63, 182, 190, 3, 122, 18, 189, 55, 162, 151, 24, 17, 14, 249, 116, 98, 76, 139 });
        }
    }
}
