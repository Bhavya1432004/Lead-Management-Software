using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;

namespace LMSWebAPI.Models
{
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("user_name")]
        public string UserName { get; set; }
        [Required]
        [Column("user_email")]
        public string UserEmail { get; set; }

        [Required]
        [Column("user_password")]
        public string UserPassword { get; set; }

        [Required]
        [Column("role", TypeName = "varchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }

        [Column("contact_phone")]
        public string ContactPhone { get; set; }
    }
}
