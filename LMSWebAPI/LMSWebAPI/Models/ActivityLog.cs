using LMSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lms.Models;
public class ActivityLog
{
    [Key]
    [Column("activity_id")]
    public int ActivityId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("lead_id")]
    public int? LeadId { get; set; }

    [Column("action_typee", TypeName = "varchar(50)")]
    public ActionType ActionType { get; set; }

    [Column("action_date")]
    public DateTime ActionDate { get; set; }

    //[JsonIgnore]
    //public virtual Lead Lead { get; set; } = null!;

    //[JsonIgnore]
    //public virtual User UIdNavigation { get; set; } = null!;
}