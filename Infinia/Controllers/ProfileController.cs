using Infinia.Core.Contracts;
using Infinia.Core.ViewModels;
using Infinia.Core.ViewModels.Profile;
using Infinia.Extensions;
using Infinia.Infrastructure.Data.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Infinia.Core.MessageConstants.ErrorMessages;

namespace Infinia.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SignInManager<Customer> signInManager;
        private readonly UserManager<Customer> userManager;
        private readonly IProfileService profileService;
        private readonly IEmailSenderService emailSenderService;

        public ProfileController(SignInManager<Customer> signInManager, UserManager<Customer> userManager, IProfileService profileService, IEmailSenderService emailSenderService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.profileService = profileService;
            this.emailSenderService = emailSenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError("Username", InvalidUsernameErrorMessage);
                return View(model);
            }
            var passwordCheck = await userManager.CheckPasswordAsync(user, model.Password);
            if (passwordCheck == false)
            {
                ModelState.AddModelError("Password", InvalidPasswordErrorMessage);
                return View(model);
            }
            if (await userManager.IsEmailConfirmedAsync(user) == false)
            {
                ModelState.AddModelError("", EmailNotConfirmedErrorMessage);
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", InvalidLoginAttemptErrorMessage);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }
            if (model.CurrentPassword == model.NewPassword)
            {
                ModelState.AddModelError("NewPassword", NewPasswordLikeCurrentPasswordErrorMessage);
            }
            var passwordValidation = await userManager.PasswordValidators.First().ValidateAsync(userManager, user, model.NewPassword);
            if (passwordValidation.Succeeded == false)
            {
                foreach (var error in passwordValidation.Errors)
                {
                    ModelState.AddModelError("NewPassword", error.Description);
                    return View(model);
                }
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ProfileDetails));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult ChangeUsername()
        {
            var model = new ChangeUsernameViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsername(ChangeUsernameViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }
            if (model.CurrentUsername == model.NewUsername)
            {
                ModelState.AddModelError("NewUsername", NewUsernameLikeCurrentUsernameErrorMessage);
                return View(model);
            }
            user.UserName = model.NewUsername;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ProfileDetails));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            var userId = User.GetId();
            if(!(await profileService.CustomerWithIdExistsAsync(userId)))
            {
                return BadRequest();
            }
            var model = await profileService.GetProfileDetailsAsync(userId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var userWithEmail = await userManager.FindByEmailAsync(model.Email);
            if (userWithEmail != null)
            {
                ModelState.AddModelError("Email", EmailAlreadyExistsErrorMessage);
                return View(model);
            }
            var userWithUsername = await userManager.FindByNameAsync(model.Username);
            if (userWithUsername != null)
            {
                ModelState.AddModelError("Username", UsernameAlreadyExistsErrorMessage);
                return View(model);
            }
            var user = await profileService.ReturnCustomerAsync(model);

            var passwordValidation = await userManager.PasswordValidators.First().ValidateAsync(userManager, user, model.Password);
            if (passwordValidation.Succeeded == false)
            {
                foreach (var error in passwordValidation.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                    return View(model);
                }
            }
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Profile", new { userId = user.Id, token = token }, Request.Scheme);

                var emailBody = $"<h1>Confirm Your Email</h1><p>Please confirm your email by clicking the link: <a href='{confirmationLink}'>Confirm Email</a></p>";
                await emailSenderService.SendEmailAsync(model.Email, "Email Confirmation", emailBody);
                TempData["ConfirmationMessage"] = "A confirmation email has been sent to your registered email address. Please check your inbox.";
                return RedirectToAction("RegistrationConfirmed");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        public IActionResult RegistrationConfirmed()
        {
            var confirmationMessage = TempData["ConfirmationMessage"] as string;

            if (confirmationMessage != null)
            {
                ViewBag.ConfirmationMessage = confirmationMessage;
            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest();
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
        }

        // POST: ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError("Email", "Email is required.");
                return View();
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "No user found with this email.");
                return View();
            }

            // Generate a new password that matches the password rules
            var newPassword = GenerateRandomPassword();

            // Reset the user's password
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                // Send email with the new password
                var emailBody = $"<h1>Password Reset</h1><p>Your new password is: <strong>{newPassword}</strong></p>";
                await emailSenderService.SendEmailAsync(model.Email, "Password Reset", emailBody);

                TempData["ConfirmationMessage"] = "A new password has been sent to your email.";
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // Generate a random password that fits the Identity password requirements
        private string GenerateRandomPassword(int length = 12)
        {
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*";

            var random = new Random();

            var password = new StringBuilder();

            // Add one character from each required set
            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(lowerChars[random.Next(lowerChars.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Fill remaining characters randomly
            string allChars = upperChars + lowerChars + digits + specialChars;
            for (int i = 4; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle to avoid predictable patterns
            return new string(password.ToString().ToCharArray().OrderBy(c => random.Next()).ToArray());
        }
    }

}


