using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class AddedLoanRepaymentInDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanRepayment_LoanApplications_LoanApplicationId",
                table: "LoanRepayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanRepayment",
                table: "LoanRepayment");

            migrationBuilder.RenameTable(
                name: "LoanRepayment",
                newName: "LoanRepayments");

            migrationBuilder.RenameIndex(
                name: "IX_LoanRepayment_LoanApplicationId",
                table: "LoanRepayments",
                newName: "IX_LoanRepayments_LoanApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanRepayments",
                table: "LoanRepayments",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRepayments_LoanApplications_LoanApplicationId",
                table: "LoanRepayments",
                column: "LoanApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanRepayments_LoanApplications_LoanApplicationId",
                table: "LoanRepayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanRepayments",
                table: "LoanRepayments");

            migrationBuilder.RenameTable(
                name: "LoanRepayments",
                newName: "LoanRepayment");

            migrationBuilder.RenameIndex(
                name: "IX_LoanRepayments_LoanApplicationId",
                table: "LoanRepayment",
                newName: "IX_LoanRepayment_LoanApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanRepayment",
                table: "LoanRepayment",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 16, 44, 47, 246, DateTimeKind.Local).AddTicks(1811));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 16, 44, 47, 246, DateTimeKind.Local).AddTicks(1811));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 16, 44, 47, 246, DateTimeKind.Local).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 16, 44, 47, 246, DateTimeKind.Local).AddTicks(1706));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 16, 44, 47, 246, DateTimeKind.Local).AddTicks(1794));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 16, 44, 47, 246, DateTimeKind.Local).AddTicks(1794));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07230bc7-5d58-4837-8ddb-48024cf95ce7", "AQAAAAEAACcQAAAAEEeUZ97/kn0l302QMeeywNC04SSN/GGC6MAciQG8TMnamUzCPCgmzE25R9Qe6gJOmA==", "b3c762a6-6b49-40a9-818b-c4472d6e1133" });

            migrationBuilder.AddForeignKey(
                name: "FK_LoanRepayment_LoanApplications_LoanApplicationId",
                table: "LoanRepayment",
                column: "LoanApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
