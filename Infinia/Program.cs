using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddHostedService<MonthlyFeeDeductionService>();
builder.Services.AddHostedService<LoanMonthlyRepaymentService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IBankAdministratorService, BankAdministratorService>();
builder.Services.AddSingleton<LoanMonthlyRepaymentService>();
builder.Services.AddHttpClient<ChatbotController>();

builder.Services.AddHostedService(provider => provider.GetRequiredService<LoanMonthlyRepaymentService>());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//TODO: Add configurations for Identity
builder.Services.AddIdentity<Customer, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Profile}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "chatbot",
    pattern: "{controller=Chatbot}/{action=SendMessage}/{userMessage?}",
defaults: new { controller = "Chatbot", action = "SendMessage" });
app.MapControllerRoute(
       name: "applyForLoan",
          pattern: "{controller=Loan}/{action=ApplyForLoan}/{type?}/{rate?}/");
app.MapControllerRoute(
    name: "branchAnalysis",
    pattern: "{controller=BankAdministrator}/{action=GetBranchAnalysis}/{branchName?}/{startDate?}/{endDate?}");
app.MapControllerRoute(
    name: "branchCashflowForecast",
    pattern: "{controller=BankAdministrator}/{action=ForecastCashflow}/{branchName?}");
app.MapRazorPages();
app.MapControllerRoute(
    name: "genderAnalsysi",
    pattern: "{controller=BankAdministrator}/{action=GenderAnalysis}/{branchName?}");
app.MapRazorPages();


app.Run();
