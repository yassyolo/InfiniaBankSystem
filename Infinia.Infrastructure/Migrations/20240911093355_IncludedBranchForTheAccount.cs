using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class IncludedBranchForTheAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Accounts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "Branch that the country is associated with");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Branch", "CreationDate" },
                values: new object[] { "None", new DateTime(2024, 8, 12, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8368) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Branch", "CreationDate" },
                values: new object[] { "None", new DateTime(2024, 8, 12, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8368) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Branch", "CreationDate" },
                values: new object[] { "Sliven", new DateTime(2024, 9, 11, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8291) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Branch", "CreationDate" },
                values: new object[] { "Sliven", new DateTime(2024, 9, 11, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8291) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Branch", "CreationDate" },
                values: new object[] { "Sliven", new DateTime(2024, 8, 22, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Branch", "CreationDate" },
                values: new object[] { "Sliven", new DateTime(2024, 8, 22, 12, 33, 54, 999, DateTimeKind.Local).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "744c5c13-6cdc-438a-b429-eb49d2458f5e", "AQAAAAEAACcQAAAAEPO8+qn5EAJwu6y/D+0Z1FC29zsM8U9Yty9Um01RlymdTCZJKTXo7+gyv0iQco+93g==", "0f323fca-3ecb-434a-826f-c8c934b4be28" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Accounts");

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
                column: "CreationDate",
                value: new DateTime(2024, 8, 11, 23, 49, 56, 204, DateTimeKind.Local).AddTicks(9739));

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
        }
    }
}
