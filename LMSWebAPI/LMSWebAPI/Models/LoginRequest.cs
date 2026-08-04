using System.ComponentModel.DataAnnotations;

namespace LMSWebAPI.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
