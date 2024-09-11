using Infinia.Core.Contracts;
using Infinia.Core.Services;
using Infinia.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Infinia.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly IProfileService profileService;

        public NotificationsController(INotificationService notificationService, IProfileService profileService)
        {
            this.notificationService = notificationService;
            this.profileService = profileService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AllNotifications()
        {
            var userId = User.GetId();
            if (await profileService.CustomerWithIdExistsAsync(userId) == false)
            {
                return BadRequest();
            }
            var model = await notificationService.GetAllNotificationsAsync(userId);
            return View(model);
        }
    }
}
