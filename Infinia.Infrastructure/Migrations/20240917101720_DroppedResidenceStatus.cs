using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class DroppedResidenceStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthsInResidence",
                table: "PropertyStatuses");

            migrationBuilder.DropColumn(
                name: "ResidenceStatus",
                table: "PropertyStatuses");

            migrationBuilder.DropColumn(
                name: "YearsInResidence",
                table: "PropertyStatuses");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 13, 17, 20, 274, DateTimeKind.Local).AddTicks(6203));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 13, 17, 20, 274, DateTimeKind.Local).AddTicks(6203));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 13, 17, 20, 274, DateTimeKind.Local).AddTicks(6134));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 13, 17, 20, 274, DateTimeKind.Local).AddTicks(6134));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 13, 17, 20, 274, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 13, 17, 20, 274, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e59cda0d-6d9b-4853-9bb8-36e242b18197", "AQAAAAEAACcQAAAAELjIMaSALOxMQIU339/X2PwfsKOHB1FBMnkj2nXuFHQvXlJwCMfoMXbPQyafd6rh7g==", "c3916be8-9a79-420c-81c5-0b71529a6b12" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthsInResidence",
                table: "PropertyStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Months in residence");

            migrationBuilder.AddColumn<string>(
                name: "ResidenceStatus",
                table: "PropertyStatuses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "Residence status");

            migrationBuilder.AddColumn<int>(
                name: "YearsInResidence",
                table: "PropertyStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Years in residence");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 13, 0, 29, 211, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 13, 0, 29, 211, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 13, 0, 29, 211, DateTimeKind.Local).AddTicks(6163));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 13, 0, 29, 211, DateTimeKind.Local).AddTicks(6163));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 13, 0, 29, 211, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 13, 0, 29, 211, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9082b0f9-846a-4768-9112-0caa663c8853", "AQAAAAEAACcQAAAAEKAHInML8nXOC7sDoCZjlLNrTqVOq5F7EATT60QyNbkLob39XlrMM9Re7LxDyMBcTQ==", "cad15a37-923b-4abc-9758-f1ff152a4682" });
        }
    }
}
