using System.ComponentModel.DataAnnotations;

namespace Elect.Test.AspNetCore.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}