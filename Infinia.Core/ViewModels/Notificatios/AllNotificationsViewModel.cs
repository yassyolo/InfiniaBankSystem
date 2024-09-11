using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Infinia.Core.ViewModels.Notificatios
{
    public class AllNotificationsViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public string CreationDate { get; set; } = string.Empty;
    }
}
