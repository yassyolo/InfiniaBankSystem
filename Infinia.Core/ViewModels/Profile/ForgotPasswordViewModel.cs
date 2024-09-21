using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infinia.Core.MessageConstants.ErrorMessages;

namespace Infinia.Core.ViewModels.Profile
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        public string Email { get; set; } = string.Empty;
    }
}
