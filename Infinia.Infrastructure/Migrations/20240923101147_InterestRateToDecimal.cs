using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class InterestRateToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "LoanApplications",
                type: "decimal(18,4)",
                nullable: false,
                comment: "Interest rate",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "Interest rate");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 24, 13, 11, 46, 885, DateTimeKind.Local).AddTicks(1279));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 24, 13, 11, 46, 885, DateTimeKind.Local).AddTicks(1279));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 23, 13, 11, 46, 885, DateTimeKind.Local).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 23, 13, 11, 46, 885, DateTimeKind.Local).AddTicks(980));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 9, 3, 13, 11, 46, 885, DateTimeKind.Local).AddTicks(1088));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 9, 3, 13, 11, 46, 885, DateTimeKind.Local).AddTicks(1088));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd65594d-88ac-49f4-a692-9fbc1b6d9d21", "AQAAAAEAACcQAAAAEJkO2tS4QqSoVqZtGa0TMPljltRsdUXY0E64IyRG/A0oMVXxxDj/Z3BodhhRkHgLjw==", "bc6f42b1-d4e4-41c9-8b20-dfab6b7e4b81" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "InterestRate",
                table: "LoanApplications",
                type: "float",
                nullable: false,
                comment: "Interest rate",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldComment: "Interest rate");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 20, 26, 48, 41, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 20, 26, 48, 41, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 20, 26, 48, 41, DateTimeKind.Local).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 20, 26, 48, 41, DateTimeKind.Local).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 20, 26, 48, 41, DateTimeKind.Local).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 20, 26, 48, 41, DateTimeKind.Local).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81f3e6af-5cb9-46f4-a2b0-39040cec1acb", "AQAAAAEAACcQAAAAEJx8nz/KI9FxiEJNenzuH4nNvfj24vk1oT41tpytW/prcvPvKJGiTht9J7EHWWXFHQ==", "7dcc0a1c-f34d-4d71-a4f9-0ca1a2d58669" });
        }
    }
}
