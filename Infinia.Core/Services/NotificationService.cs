using Infinia.Core.Contracts;
using Infinia.Core.ViewModels.Notificatios;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infinia.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository repository;

        public NotificationService(IRepository repository)
        {
            this.repository = repository;
        }
        //TODO: Implement the GetAllNotificationsAsync method
        public async Task<IEnumerable<AllNotificationsViewModel>?> GetAllNotificationsAsync(string userId)
        {
            var notifications = await repository.AllReadOnly<Notification>()
                .Where(x => x.CustomerId == userId)
                .Select(x => new AllNotificationsViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    CreationDate = x.CreationDate.ToString("dd.MM.yyyy"),
                }).ToListAsync();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            await repository.SaveChangesAsync();
            return notifications;
        }
    }
}
