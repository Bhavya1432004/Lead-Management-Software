using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMSWebAPI.Models
{
    public class Lead
    {
        [Key]
        [Column("lead_id")]
        public int LeadId { get; set; }
        
        [Required]
        [Column("lead_name")]
        public string LeadName { get; set; } = null!;

        [Required]
        [Column("lead_email")]
        public string LeadEmail { get; set; } = null!;

        [Column("lead_phone")]
        public string? LeadPhone { get; set; }

        [Required] 
        [Column("lead_source")]
        public string LeadSource { get; set; } = null!;

        [Column("assigned_to_user_id")]
        public int AssignedToUserId { get; set; }

        [Required]
        [Column("status",TypeName ="varchar(50)")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LeadStatus Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

        //[JsonIgnore]
        //public virtual User AssignedToNavigation { get; set; } = null!;

        //[JsonIgnore]
        //public virtual ICollection<LeadAssignment> LeadAssignments { get; set; } = new List<LeadAssignment>();

        //[JsonIgnore]
        //public virtual ICollection<LeadLog> LeadLogs { get; set; } = new List<LeadLog>();
    }
}
