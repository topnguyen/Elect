using System.ComponentModel.DataAnnotations;

namespace Elect.Test.AspNetCore.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}