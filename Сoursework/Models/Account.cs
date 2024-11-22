using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursework.Models
{
    public class Account : IdentityUser
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string? ConfirmPassword { get; set; } // Поле для подтверждения пароля
    }
    public class AccountUpdateDto
    {
        public string? Name { get; set; }
        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }
    }
}