using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinia.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Address identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Street address, including house number"),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "City name"),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Country name"),
                    PostalCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false, comment: "Postal or zip code")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                },
                comment: "Address entity");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Education identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Education level")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                },
                comment: "Education entity");

            migrationBuilder.CreateTable(
                name: "EmployerInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Employer information identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRetired = table.Column<bool>(type: "bit", nullable: false, comment: "Is the person retired"),
                    EmployerName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Employer name"),
                    Position = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Position"),
                    YearsAtJob = table.Column<int>(type: "int", nullable: false, comment: "Years at job"),
                    MonthsAtJob = table.Column<int>(type: "int", nullable: false, comment: "Months at job"),
                    TotalWorkExperienceYears = table.Column<int>(type: "int", nullable: false, comment: "Total work experience years"),
                    TotalWorkExperienceMonths = table.Column<int>(type: "int", nullable: false, comment: "Total work experience months")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerInfos", x => x.Id);
                },
                comment: "Employer information entity");

            migrationBuilder.CreateTable(
                name: "HouselholdInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Household information identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfHouseholdMembers = table.Column<int>(type: "int", nullable: false, comment: "Number of household members"),
                    MembersWithProvenIncome = table.Column<int>(type: "int", nullable: false, comment: "Number of household members with proven income"),
                    Dependents = table.Column<int>(type: "int", nullable: false, comment: "Number of dependents that depend on the income of the applicant"),
                    FamilyMembersCount = table.Column<int>(type: "int", nullable: false, comment: "Number of family members")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouselholdInfos", x => x.Id);
                },
                comment: "Household information entity");

            migrationBuilder.CreateTable(
                name: "IdentityCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identity card identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncryptedCardNumber = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Identity card number"),
                    EncryptedDateOfIssue = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Identity card date of issue"),
                    EncryptedIssuer = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Identity card issuer"),
                    EncryptedNationality = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Identity card nationality"),
                    EncryptedSex = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Identity card sex")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityCards", x => x.Id);
                },
                comment: "Identity card entity");

            migrationBuilder.CreateTable(
                name: "IncomeInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Income information identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NetMonthlyIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Net monthly income"),
                    FixedMonthlyExpenses = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Fixed monthly expenses"),
                    PermanentContractIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Permanent contract income"),
                    TemporaryContractIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Temporary contract income"),
                    CivilContractIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Civil contract income"),
                    BusinessIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Business income"),
                    PensionIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Pension income"),
                    FreelanceIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Freelance income"),
                    OtherIncome = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Other income"),
                    HasOtherCredits = table.Column<bool>(type: "bit", nullable: false, comment: "Has other credits")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeInfos", x => x.Id);
                },
                comment: "Income information entity");

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Marital status identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Marital status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                },
                comment: "Marital status entity");

            migrationBuilder.CreateTable(
                name: "PropertyStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Property status identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasApartmentOrHouse = table.Column<bool>(type: "bit", nullable: false, comment: "Has apartment or house"),
                    HasCommercialProperty = table.Column<bool>(type: "bit", nullable: false, comment: "Has commercial property"),
                    HasLand = table.Column<bool>(type: "bit", nullable: false, comment: "Has land"),
                    HasMultipleProperties = table.Column<bool>(type: "bit", nullable: false, comment: "Has multiple properties"),
                    HasPartialOwnership = table.Column<bool>(type: "bit", nullable: false, comment: "Has partial ownership"),
                    NoProperty = table.Column<bool>(type: "bit", nullable: false, comment: "No property"),
                    VehicleCount = table.Column<int>(type: "int", nullable: false, comment: "Vehicle count"),
                    ResidenceStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Residence status"),
                    YearsInResidence = table.Column<int>(type: "int", nullable: false, comment: "Years in residence"),
                    MonthsInResidence = table.Column<int>(type: "int", nullable: false, comment: "Months in residence")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyStatuses", x => x.Id);
                },
                comment: "Property status entity");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Customer name from identity card"),
                    IdentityCardId = table.Column<int>(type: "int", nullable: false, comment: "Customer identity card identifier"),
                    AddressId = table.Column<int>(type: "int", nullable: false, comment: "Customer address identifier"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_IdentityCards_IdentityCardId",
                        column: x => x.IdentityCardId,
                        principalTable: "IdentityCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Customer entity");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Account identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncryptedIBAN = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Account number"),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Account type"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "Account name"),
                    Balance = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Account balance"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Account creation date"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "customer account identifier"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Account status"),
                    MonthlyFee = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Account monthly fee")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Account entity");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Loan application identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationId = table.Column<int>(type: "int", nullable: false, comment: "Education identifier"),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false, comment: "Marital status identifier"),
                    HouseholdInfoId = table.Column<int>(type: "int", nullable: false, comment: "Household information identifier"),
                    PropertyStatusId = table.Column<int>(type: "int", nullable: false, comment: "Property status identifier"),
                    EmployerInfoId = table.Column<int>(type: "int", nullable: false, comment: "Employer information identifier"),
                    IncomeInfoId = table.Column<int>(type: "int", nullable: false, comment: "Income information identifier"),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Loan application date"),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Loan amount"),
                    LoanTermMonths = table.Column<int>(type: "int", nullable: false, comment: "Loan term in months"),
                    InterestRate = table.Column<double>(type: "float", nullable: false, comment: "Interest rate"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Loan application customer identifier"),
                    AccountId = table.Column<int>(type: "int", nullable: false, comment: "Loan application account identifier"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Loan application status")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplications_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_EmployerInfos_EmployerInfoId",
                        column: x => x.EmployerInfoId,
                        principalTable: "EmployerInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_HouselholdInfos_HouseholdInfoId",
                        column: x => x.HouseholdInfoId,
                        principalTable: "HouselholdInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_IncomeInfos_IncomeInfoId",
                        column: x => x.IncomeInfoId,
                        principalTable: "IncomeInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanApplications_PropertyStatuses_PropertyStatusId",
                        column: x => x.PropertyStatusId,
                        principalTable: "PropertyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Loan application entity");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Transaction identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Transaction type"),
                    ReceiverName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Full name of the receiver of the transaction"),
                    EncryptedReceiverIBAN = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "IBAN of the receiver"),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Transaction amount"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Transaction description"),
                    Reason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Transaction reason"),
                    AccountId = table.Column<int>(type: "int", nullable: false, comment: "Transaction account identifier"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Transaction date"),
                    Bic = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Transaction BIC"),
                    Fee = table.Column<decimal>(type: "decimal(18,4)", nullable: false, comment: "Transaction fee")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Transaction entity");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityCardId",
                table: "AspNetUsers",
                column: "IdentityCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_AccountId",
                table: "LoanApplications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_CustomerId",
                table: "LoanApplications",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_EducationId",
                table: "LoanApplications",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_EmployerInfoId",
                table: "LoanApplications",
                column: "EmployerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_HouseholdInfoId",
                table: "LoanApplications",
                column: "HouseholdInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_IncomeInfoId",
                table: "LoanApplications",
                column: "IncomeInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_MaritalStatusId",
                table: "LoanApplications",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_PropertyStatusId",
                table: "LoanApplications",
                column: "PropertyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LoanApplications");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "EmployerInfos");

            migrationBuilder.DropTable(
                name: "HouselholdInfos");

            migrationBuilder.DropTable(
                name: "IncomeInfos");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "PropertyStatuses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "IdentityCards");
        }
    }
}
