using Lms.Models;
using LMSWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LMSWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        //internal IEnumerable<object> Users;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Lead> leads { get; set; }

        public DbSet<LeadLog> lead_log { get; set; }

        public DbSet<LeadAssignment> lead_assignment { get; set; }

        public DbSet<ActivityLog> activity_log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //-------USER ENTITY---------
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ContactPhone)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Role)
                    .HasConversion(v => v.ToString(), v => (UserRole)Enum.Parse(typeof(UserRole), v))
                    .HasColumnType("varchar(50)");

                entity.HasIndex(e => e.UserName).IsUnique();
                entity.HasIndex(e => e.UserEmail).IsUnique();
            });


            //-------LEAD ENTITY---------

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.ToTable("leads");

                entity.HasKey(e => e.LeadId);

                entity.Property(e => e.LeadId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LeadName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LeadEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.LeadPhone)
                    .HasMaxLength(15)
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.LeadSource)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Status)
                    .HasConversion(v => v.ToString(), v => (LeadStatus)Enum.Parse(typeof(LeadStatus), v))
                    .HasColumnType("varchar(50)")
                    .HasDefaultValue(LeadStatus.New);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.AssignedToUserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_leads_users_assigned_to");

                entity.HasIndex(e => e.AssignedToUserId);

            });

            //-------LEAD LOG ENTITY---------

            modelBuilder.Entity<LeadLog>(entity =>
            {
                entity.ToTable("lead_log");

                entity.HasKey(e => e.status_id);

                entity.Property(e => e.status_id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.lead_id)
                    .IsRequired();

                entity.Property(e => e.old_status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.new_status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)")
                    .HasConversion<string>();

                entity.Property(e => e.update_by)
                    .IsRequired();

                entity.Property(e => e.update_date)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                //ForeignKeys

                entity.HasOne<Lead>()
                    .WithMany()
                    .HasForeignKey(e => e.lead_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_lead_log_leads");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.update_by)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_lead_log_users");

                entity.HasIndex(e => e.lead_id);
                entity.HasIndex(e => e.update_by);
            });

            //-------LEAD ASSIGNMENT ENTITY---------
            modelBuilder.Entity<LeadAssignment>(entity =>
            {
                entity.ToTable("lead_assignment");

                entity.HasKey(e => e.AssignmentId);

                entity.Property(e => e.AssignmentId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LeadId)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.AssignmentDate)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");


                //ForeignKeys
                entity.HasOne<Lead>()
                    .WithMany()
                    .HasForeignKey(e => e.LeadId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_lead_assignment_leads");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_lead_assignment_users");


                entity.HasIndex(e => e.LeadId);
                entity.HasIndex(e => e.UserId);
            });

            //-------Activity Log Entity---------
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("activity_log");

                entity.HasKey(e => e.ActivityId);

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.LeadId)
                    .IsRequired(false);

                entity.Property(e => e.ActionType)
                    .HasConversion<string>()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ActionDate)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_activity_log_users");

                entity.HasOne<Lead>()
                    .WithMany()
                    .HasForeignKey(e => e.LeadId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_activity_log_leads");

                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.LeadId);
                entity.HasIndex(e => e.ActionDate);
            });
        }
    }
}
