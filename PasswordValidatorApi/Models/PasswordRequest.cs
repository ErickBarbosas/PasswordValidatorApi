using System.ComponentModel.DataAnnotations;

namespace PasswordValidatorApi.Models
{
    public class PasswordRequest
    {
        [Required]
        public string Password { get; set; }
    }
}
