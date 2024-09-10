using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "PostalCode", "Street" },
                values: new object[] { 1, "Sliven", "Bulgaria", "8800", "Sini kamani 28" });

            migrationBuilder.InsertData(
                table: "IdentityCards",
                columns: new[] { "Id", "EncryptedCardNumber", "EncryptedDateOfIssue", "EncryptedIssuer", "EncryptedNationality", "EncryptedSex" },
                values: new object[] { 1, new byte[] { 49, 34, 67, 29, 133, 160, 202, 196, 145, 181, 192, 114, 229, 113, 58, 22, 193, 50, 4, 2, 210, 21, 75, 154, 140, 102, 163, 28, 86, 92, 151, 73 }, new byte[] { 179, 85, 117, 239, 66, 92, 60, 97, 70, 3, 196, 223, 92, 112, 85, 69, 175, 173, 235, 42, 60, 70, 176, 166, 214, 41, 252, 156, 139, 109, 140, 130 }, new byte[] { 158, 114, 169, 139, 148, 7, 21, 70, 47, 14, 123, 36, 201, 172, 203, 230, 58, 200, 67, 15, 127, 249, 232, 154, 203, 237, 132, 80, 47, 222, 90, 250 }, new byte[] { 134, 55, 67, 164, 48, 44, 210, 254, 78, 218, 227, 4, 6, 97, 152, 180, 234, 106, 141, 247, 74, 13, 39, 26, 219, 173, 161, 238, 61, 183, 169, 221 }, new byte[] { 139, 175, 173, 247, 127, 244, 248, 147, 1, 211, 148, 110, 2, 137, 63, 182, 190, 3, 122, 18, 189, 55, 162, 151, 24, 17, 14, 249, 116, 98, 76, 139 } });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AddressId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IdentityCardId", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137", 0, 1, "e18a347f-0275-4698-9b03-ec4f2338e14e", "ivanivanov@gmail.com", true, 1, false, null, "Ivan Ivanov", "IVANIBANOV@GMAIL.COM", "IVANIVANOV", "AQAAAAEAACcQAAAAED7aT9Cna9YG79QmfDDLYMES027CxYTo6w/JGIQTfB4eG8jHgG50wPU8QmmTBFiKaQ==", null, false, "3d8c4a0b-af73-403f-bc60-0e70ddcd543a", false, "ivanivanov" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "CreationDate", "CustomerId", "EncryptedIBAN", "MonthlyFee", "Name", "Status", "Type" },
                values: new object[,]
                {
                    { 1, 100000000m, new DateTime(2024, 8, 11, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8663), "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137", new byte[] { 31, 206, 109, 135, 141, 71, 209, 126, 42, 101, 231, 236, 67, 171, 245, 18, 100, 92, 210, 218, 76, 159, 101, 79, 14, 131, 4, 157, 91, 180, 162, 152, 128, 221, 107, 227, 194, 167, 126, 176, 76, 155, 164, 21, 230, 73, 221, 13 }, 0m, "Bank account", "Open", "Bank" },
                    { 2, 1000000m, new DateTime(2024, 8, 11, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8663), "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137", new byte[] { 31, 206, 109, 135, 141, 71, 209, 126, 42, 101, 231, 236, 67, 171, 245, 18, 100, 92, 210, 218, 76, 159, 101, 79, 14, 131, 4, 157, 91, 180, 162, 152, 128, 221, 107, 227, 194, 167, 126, 176, 76, 155, 164, 21, 230, 73, 221, 13 }, 0m, "Savings account for bank account", "Open", "Savings" },
                    { 3, 1000m, new DateTime(2024, 9, 10, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8464), "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137", new byte[] { 37, 24, 39, 194, 187, 85, 25, 67, 195, 56, 247, 137, 17, 220, 32, 81, 144, 82, 184, 28, 19, 253, 96, 177, 255, 107, 24, 58, 20, 70, 223, 151, 177, 120, 40, 127, 208, 127, 249, 54, 75, 220, 180, 194, 3, 193, 139, 189 }, 2m, "Current acount", "Open", "Current" },
                    { 4, 200m, new DateTime(2024, 9, 10, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8464), "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137", new byte[] { 37, 24, 39, 194, 187, 85, 25, 67, 195, 56, 247, 137, 17, 220, 32, 81, 144, 82, 184, 28, 19, 253, 96, 177, 255, 107, 24, 58, 20, 70, 223, 151, 177, 120, 40, 127, 208, 127, 249, 54, 75, 220, 180, 194, 3, 193, 139, 189 }, 2m, "Savings account", "Open", "Savings" },
                    { 5, 900m, new DateTime(2024, 8, 21, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8522), "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137", new byte[] { 193, 10, 140, 103, 113, 25, 240, 73, 189, 131, 84, 240, 133, 51, 134, 120, 251, 105, 160, 55, 185, 161, 253, 175, 31, 209, 58, 112, 169, 238, 137, 250, 196, 39, 48, 190, 187, 42, 71, 23, 125, 144, 117, 50, 69, 224, 12, 122 }, 2m, "Current acount-2", "Open", "Current" },
                    { 6, 100m, new DateTime(2024, 8, 21, 2, 49, 11, 576, DateTimeKind.Local).AddTicks(8522), "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137", new byte[] { 193, 10, 140, 103, 113, 25, 240, 73, 189, 131, 84, 240, 133, 51, 134, 120, 251, 105, 160, 55, 185, 161, 253, 175, 31, 209, 58, 112, 169, 238, 137, 250, 196, 39, 48, 190, 187, 42, 71, 23, 125, 144, 117, 50, 69, 224, 12, 122 }, 2m, "Savings account-2", "Open", "Savings" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2cbfb88d - c6a3 - 445e-a0f3 - 2492fbbbc137");

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityCards",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
