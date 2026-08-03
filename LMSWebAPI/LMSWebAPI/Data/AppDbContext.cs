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

                entity.HasKey(e => e.u_id);

                entity.Property(e => e.u_id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.u_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.u_email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.u_password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.contact_no)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.role)
                    .HasConversion(v => v.ToString(), v => (UserRole)Enum.Parse(typeof(UserRole), v))
                    .HasColumnType("varchar(50)");

                entity.HasIndex(e => e.u_name).IsUnique();
                entity.HasIndex(e => e.u_email).IsUnique();
            });


            //-------LEAD ENTITY---------

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.ToTable("leads");

                entity.HasKey(e => e.lead_id);

                entity.Property(e => e.lead_id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.lead_name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.lead_email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.lead_contact)
                    .HasMaxLength(15)
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.lead_source)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.lead_status)
                    .HasConversion(v => v.ToString(), v => (LeadStatus)Enum.Parse(typeof(LeadStatus), v))
                    .HasColumnType("varchar(50)")
                    .HasDefaultValue(LeadStatus.New);

                entity.Property(e => e.created_at)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.update_at)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.assigned_to)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_leads_users_assigned_to");

                entity.HasIndex(e => e.assigned_to);

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

                entity.HasKey(e => e.assignment_id);

                entity.Property(e => e.assignment_id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.lead_id)
                    .IsRequired();

                entity.Property(e => e.u_id)
                    .IsRequired();

                entity.Property(e => e.assignment_date)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");


                //ForeignKeys
                entity.HasOne<Lead>()
                    .WithMany()
                    .HasForeignKey(e => e.lead_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_lead_assignment_leads");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.u_id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_lead_assignment_users");


                entity.HasIndex(e => e.lead_id);
                entity.HasIndex(e => e.u_id);
            });

            //-------Activity Log Entity---------
            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("activity_log");

                entity.HasKey(e => e.activity_id);

                entity.Property(e => e.activity_id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.u_id)
                    .IsRequired();

                entity.Property(e => e.lead_id)
                    .IsRequired(false);

                entity.Property(e => e.action_type)
                    .HasConversion<string>()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.action_date)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.u_id)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_activity_log_users");

                entity.HasOne<Lead>()
                    .WithMany()
                    .HasForeignKey(e => e.lead_id)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_activity_log_leads");

                entity.HasIndex(e => e.u_id);
                entity.HasIndex(e => e.lead_id);
                entity.HasIndex(e => e.action_date);
            });
        }
    }
}
