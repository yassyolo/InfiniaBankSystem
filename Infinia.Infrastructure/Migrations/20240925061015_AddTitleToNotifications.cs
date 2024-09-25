using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class AddTitleToNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                comment: "Notification title");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notifications");

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
    }
}
