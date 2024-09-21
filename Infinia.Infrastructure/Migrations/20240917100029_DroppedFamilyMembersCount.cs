using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class DroppedFamilyMembersCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyMembersCount",
                table: "HouselholdInfos");

            migrationBuilder.AlterColumn<string>(
                name: "Bic",
                table: "Transactions",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                comment: "Transaction BIC",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Transaction BIC");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Bic",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Transaction BIC",
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true,
                oldComment: "Transaction BIC");

            migrationBuilder.AddColumn<int>(
                name: "FamilyMembersCount",
                table: "HouselholdInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Number of family members");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 12, 19, 21, 6, 213, DateTimeKind.Local).AddTicks(5512));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 12, 19, 21, 6, 213, DateTimeKind.Local).AddTicks(5512));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 11, 19, 21, 6, 213, DateTimeKind.Local).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 11, 19, 21, 6, 213, DateTimeKind.Local).AddTicks(5381));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 22, 19, 21, 6, 213, DateTimeKind.Local).AddTicks(5447));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 22, 19, 21, 6, 213, DateTimeKind.Local).AddTicks(5447));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d42de6f9-7080-4a4f-8feb-aba781a30d73", "AQAAAAEAACcQAAAAEI7TgEl65I903xhTqveQmcwxCOe5gEtqdTe0fbbAxj13DtPmZec4njMyTh0tVQ6Aaw==", "7d20c3f3-609c-4b98-8571-214e5aa47b45" });
        }
    }
}
