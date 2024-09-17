using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class RemoveRequiredLoanRepaymentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LoanRepayment",
                type: "datetime2",
                nullable: true,
                comment: "Repayment date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Repayment date");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "LoanRepayment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Repayment date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Repayment date");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 13, 59, 51, 862, DateTimeKind.Local).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 18, 13, 59, 51, 862, DateTimeKind.Local).AddTicks(4826));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 13, 59, 51, 862, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 17, 13, 59, 51, 862, DateTimeKind.Local).AddTicks(4755));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 13, 59, 51, 862, DateTimeKind.Local).AddTicks(4811));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 28, 13, 59, 51, 862, DateTimeKind.Local).AddTicks(4811));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4d35ba9-bf1f-44b2-8b6e-6ca5b515957b", "AQAAAAEAACcQAAAAEBXcx7XT643roODtK62IAH+sTDi/G+n9/afOOyrCQSdmzR1WCEnotm5/x3tLP688zg==", "0117d954-0725-49bb-9885-ec51e1a0a519" });
        }
    }
}
