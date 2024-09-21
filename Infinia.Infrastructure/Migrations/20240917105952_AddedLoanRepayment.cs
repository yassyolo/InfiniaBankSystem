using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class AddedLoanRepayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanRepaymentNumber",
                table: "LoanApplications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Loan repayment number");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "LoanApplications",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                comment: "Loan application type");

            migrationBuilder.CreateTable(
                name: "LoanRepayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Loan repayment identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationId = table.Column<int>(type: "int", nullable: false, comment: "Loan application identifier"),
                    RepaymentAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Repayment amount"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Repayment date"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Repayment status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRepayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanRepayment_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalTable: "LoanApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Loan repayment entity");

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

            migrationBuilder.CreateIndex(
                name: "IX_LoanRepayment_LoanApplicationId",
                table: "LoanRepayment",
                column: "LoanApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanRepayment");

            migrationBuilder.DropColumn(
                name: "LoanRepaymentNumber",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "LoanApplications");

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
    }
}
