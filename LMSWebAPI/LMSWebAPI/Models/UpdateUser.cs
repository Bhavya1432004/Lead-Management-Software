using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace LMSWebAPI.Models
{
    public class UpdateUser
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }
        
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }
        public UserRole Role { get; set; }

        public string ContactPhone { get; set; }

    }
}
