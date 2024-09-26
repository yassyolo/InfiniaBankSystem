using Infinia.Core.Contracts;
using Infinia.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infinia.Controllers

{
    [Authorize]
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

        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await notificationService.MarkAsReadAsync(id);
            return RedirectToAction(nameof(AllNotifications));
        }
    }
}
