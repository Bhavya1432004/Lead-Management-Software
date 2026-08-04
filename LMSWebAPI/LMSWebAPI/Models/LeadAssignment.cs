using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSWebAPI.Models
{
    public class LeadAssignment
    {
        [Key]
        [Column("assignment_id")]
        public int AssignmentId { get; set; }

        [Required]
        [Column("lead_id")]
        public int LeadId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("assignment_date")]
        public DateTime AssignmentDate { get; set; }
    }
}
