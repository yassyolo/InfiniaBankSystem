using Infinia.Core.Contracts;
using Infinia.Core.ViewModels;
using Infinia.Extensions;
using Infinia.Infrastructure.Data.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using static Infinia.Core.MessageConstants.ErrorMessages;

namespace Infinia.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SignInManager<Customer> signInManager;
        private readonly UserManager<Customer> userManager;
        private readonly IProfileService profileService;

        public ProfileController(SignInManager<Customer> signInManager, UserManager<Customer> userManager, IProfileService profileService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.profileService = profileService;
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
            return RedirectToAction(nameof(Login));
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
            if(await profileService.CustomerWithIdExistsAsync(userId))
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
            if (userManager != null)
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
                return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}

