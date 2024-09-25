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
                .Where(x => x.CustomerId == userId && x.IsRead == false)
                .Select(x => new AllNotificationsViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    CreationDate = x.CreationDate.ToString("dd.MMMM.yyyy"),
                    Title = x.Title,
                }).ToListAsync();
            await repository.SaveChangesAsync();
            return notifications;
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notification = await repository.All<Notification>().FirstOrDefaultAsync(x => x.Id == id);
            notification.IsRead = true;
            await repository.SaveChangesAsync();
        }
    }
}
