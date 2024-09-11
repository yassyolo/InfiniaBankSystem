using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class AddedNotificationsAndLastMonthlyFeeDeduction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastMonthlyFeeDeduction",
                table: "Accounts",
                type: "datetime2",
                nullable: true,
                comment: "Last time a monthly fee was deducted");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Notification identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Customer notification identifier"),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Notification message"),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, comment: "Notification status"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Notification creation date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Notification entity");

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CustomerId",
                table: "Notifications",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropColumn(
                name: "LastMonthlyFeeDeduction",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2024, 8, 12, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2024, 8, 12, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2024, 9, 11, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8291));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2024, 9, 11, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8291));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2024, 8, 22, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8350));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2024, 8, 22, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8350));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "744c5c13-6cdc-438a-b429-eb49d2458f5e", "AQAAAAEAACcQAAAAEPO8+qn5EAJwu6y/D+0Z1FC29zsM8U9Yty9Um01RlymdTCZJKTXo7+gyv0iQco+93g==", "0f323fca-3ecb-434a-826f-c8c934b4be28" });
        }
    }
}
