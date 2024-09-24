using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class AddedCreditScoreToLoans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CreditScore",
                table: "LoanApplications",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Loan application credit score");

            migrationBuilder.AddColumn<double>(
                name: "ProbabilityOfApproval",
                table: "LoanApplications",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Loan application approval rate");

            migrationBuilder.AddColumn<string>(
                name: "RiskGroup",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Loan application risk group");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 25, 13, 48, 26, 524, DateTimeKind.Local).AddTicks(3181));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 25, 13, 48, 26, 524, DateTimeKind.Local).AddTicks(3181));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 24, 13, 48, 26, 524, DateTimeKind.Local).AddTicks(3088));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 24, 13, 48, 26, 524, DateTimeKind.Local).AddTicks(3088));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 9, 4, 13, 48, 26, 524, DateTimeKind.Local).AddTicks(3157));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 9, 4, 13, 48, 26, 524, DateTimeKind.Local).AddTicks(3157));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c7c9f82-8e51-47d6-bef1-4f4fea586751", "AQAAAAEAACcQAAAAEFL5qXyXPUhF3t7K/1T2pDf9WphEVSlSRDscJ+eZDp2EsAL2S+WDjypb4oxZ3GC5mQ==", "7f55c022-2d6a-4fda-89d0-5a1fea137102" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditScore",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "ProbabilityOfApproval",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "RiskGroup",
                table: "LoanApplications");

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
    }
}
