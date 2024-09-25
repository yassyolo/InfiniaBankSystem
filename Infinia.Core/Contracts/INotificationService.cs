
using Infinia.Core.ViewModels.Notificatios;

namespace Infinia.Core.Contracts
{
    public interface INotificationService
    {
        Task<IEnumerable<AllNotificationsViewModel>?> GetAllNotificationsAsync(string userId);
        Task MarkAsReadAsync(int id);
    }
}
