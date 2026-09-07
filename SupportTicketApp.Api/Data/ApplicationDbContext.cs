using Microsoft.EntityFrameworkCore;
using SupportTicketApp.Api.Models;

namespace SupportTicketApp.Api.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(customer => customer.CustomerId)
                .HasName("PK_Customers");

            entity.HasIndex(customer => customer.Email)
                .IsUnique()
                .HasDatabaseName("UQ_Customers_Email");

            entity.Property(customer => customer.CompanyName)
                .HasMaxLength(150);

            entity.Property(customer => customer.ContactName)
                .HasMaxLength(100);

            entity.Property(customer => customer.Email)
                .HasMaxLength(255);

            entity.Property(customer => customer.PhoneNumber)
                .HasMaxLength(25);

            entity.Property(customer => customer.IsActive)
                .HasDefaultValue(true);

            entity.Property(customer => customer.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(technician => technician.TechnicianId)
                .HasName("PK_Technicians");

            entity.HasIndex(technician => technician.Email)
                .IsUnique()
                .HasDatabaseName("UQ_Technicians_Email");

            entity.Property(technician => technician.FirstName)
                .HasMaxLength(50);

            entity.Property(technician => technician.LastName)
                .HasMaxLength(50);

            entity.Property(technician => technician.Email)
                .HasMaxLength(255);

            entity.Property(technician => technician.IsActive)
                .HasDefaultValue(true);

            entity.Property(technician => technician.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(status => status.TicketStatusId)
                .HasName("PK_TicketStatuses");

            entity.HasIndex(status => status.StatusName)
                .IsUnique()
                .HasDatabaseName("UQ_TicketStatuses_StatusName");

            entity.HasIndex(status => status.DisplayOrder)
                .IsUnique()
                .HasDatabaseName("UQ_TicketStatuses_DisplayOrder");

            entity.Property(status => status.StatusName)
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(ticket => ticket.TicketId)
                .HasName("PK_Tickets");

            entity.Property(ticket => ticket.TicketId)
                .ValueGeneratedNever();

            entity.HasIndex(ticket => ticket.CustomerId)
                .HasDatabaseName("IX_Tickets_CustomerId");

            entity.HasIndex(ticket => ticket.TechnicianId)
                .HasDatabaseName("IX_Tickets_TechnicianId");

            entity.HasIndex(ticket => ticket.TicketStatusId)
                .HasDatabaseName("IX_Tickets_TicketStatusId");

            entity.HasIndex(ticket => ticket.Priority)
                .HasDatabaseName("IX_Tickets_Priority");

            entity.Property(ticket => ticket.Title)
                .HasMaxLength(200);

            entity.Property(ticket => ticket.Priority)
                .HasMaxLength(20)
                .HasDefaultValue("Medium");

            entity.Property(ticket => ticket.CreatedAtUtc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.Property(ticket => ticket.UpdatedAtUtc)
                .HasPrecision(0);

            entity.Property(ticket => ticket.ResolvedAtUtc)
                .HasPrecision(0);

            entity.HasOne(ticket => ticket.Customer)
                .WithMany(customer => customer.Tickets)
                .HasForeignKey(ticket => ticket.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_Customers");

            entity.HasOne(ticket => ticket.Technician)
                .WithMany(technician => technician.Tickets)
                .HasForeignKey(ticket => ticket.TechnicianId)
                .HasConstraintName("FK_Tickets_Technicians");

            entity.HasOne(ticket => ticket.TicketStatus)
                .WithMany(status => status.Tickets)
                .HasForeignKey(ticket => ticket.TicketStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tickets_TicketStatuses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
