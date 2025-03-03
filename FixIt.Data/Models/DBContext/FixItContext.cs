using System;
using System.Collections.Generic;
using FixIt.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FixIt.Data.Models.DBContext;

public partial class FixItContext : DbContext
{
    public FixItContext()
    {
    }

    public FixItContext(DbContextOptions<FixItContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Urgencylevel> Urgencylevels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<FixItContext>()
            .Build();
        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Departmentid).HasName("department_pkey");

            entity.ToTable("department");

            entity.Property(e => e.Departmentid)
                .HasDefaultValueSql("nextval('department_squence'::regclass)")
                .HasColumnName("departmentid");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.Employeeid)
                .HasDefaultValueSql("nextval('employee_sequence'::regclass)")
                .HasColumnName("employeeid");
            entity.Property(e => e.Department).HasColumnName("department");
            entity.Property(e => e.Lastname)
                .HasMaxLength(30)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.DepartmentNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Department)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_employee_department");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_employee_role");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Positionid).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Positionid).HasColumnName("positionid");
            entity.Property(e => e.Department).HasColumnName("department");
            entity.Property(e => e.Description)
                .HasMaxLength(75)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");

            entity.HasOne(d => d.DepartmentNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.Department)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Statusid).HasName("status_pkey");

            entity.ToTable("status");

            entity.HasIndex(e => e.Name, "status_name_key").IsUnique();

            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Ticketid).HasName("ticket_pkey");

            entity.ToTable("ticket");

            entity.Property(e => e.Ticketid)
                .HasDefaultValueSql("nextval('ticket_sequence'::regclass)")
                .HasColumnName("ticketid");
            entity.Property(e => e.Assignedto).HasColumnName("assignedto");
            entity.Property(e => e.Closedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("closedat");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("duedate");
            entity.Property(e => e.Statusid)
                .HasDefaultValue(1)
                .HasColumnName("statusid");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Urgencyid).HasColumnName("urgencyid");

            entity.HasOne(d => d.AssignedtoNavigation).WithMany(p => p.TicketAssignedtoNavigations)
                .HasForeignKey(d => d.Assignedto)
                .HasConstraintName("ticket_assignedto_fkey");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.TicketCreatedbyNavigations)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_createdby_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Statusid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_statusid_fkey");

            entity.HasOne(d => d.Urgency).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Urgencyid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_urgencyid_fkey");
        });

        modelBuilder.Entity<Urgencylevel>(entity =>
        {
            entity.HasKey(e => e.Urgencylevelid).HasName("urgencylevel_pkey");

            entity.ToTable("urgencylevel");

            entity.HasIndex(e => e.Name, "urgencylevel_name_key").IsUnique();

            entity.Property(e => e.Urgencylevelid).HasColumnName("urgencylevelid");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Resolutiontime).HasColumnName("resolutiontime");
        });
        modelBuilder.HasSequence("department_squence")
            .StartsAt(999L)
            .IncrementsBy(2);
        modelBuilder.HasSequence("employee_sequence")
            .StartsAt(9999L)
            .IncrementsBy(3);
        modelBuilder.HasSequence("ticket_sequence").StartsAt(100L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
