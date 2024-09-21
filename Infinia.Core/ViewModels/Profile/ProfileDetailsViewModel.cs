using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
namespace Infinia.Core.ViewModels
{
    public class ProfileDetailsViewModel
    {
        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}
