using Microsoft.EntityFrameworkCore;
using ClinicManagement_API.Domains.Entities;
using ClinicManagement_API.Domains.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ClinicManagement_API.Infrastructure.Persisstence;

public class User : IdentityUser<Guid> { }
public class Role : IdentityRole<Guid> { }


public class ClinicDbContext : IdentityDbContext<User, Role, Guid>
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
    {
    }

    public DbSet<Clinic> Clinics => Set<Clinic>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<DoctorService> DoctorServices => Set<DoctorService>();
    public DbSet<DoctorAvailability> DoctorAvailabilities => Set<DoctorAvailability>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingToken> BookingTokens => Set<BookingToken>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole").HasKey(k => new { k.UserId, k.RoleId });

        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();

        modelBuilder.Entity<Clinic>(e =>
        {
            e.ToTable("Clinics");
            e.HasKey(x => x.ClinicId);
            e.Property(x => x.Code).HasMaxLength(20).IsRequired();
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.Property(x => x.TimeZone).HasMaxLength(50).HasDefaultValue("Asia/Ho_Chi_Minh");
            e.Property(x => x.Phone).HasMaxLength(20);
            e.Property(x => x.Email).HasMaxLength(256);
            e.HasIndex(x => x.Code).IsUnique();
        });

        modelBuilder.Entity<Doctor>(e =>
        {
            e.ToTable("Doctors");
            e.HasKey(x => x.DoctorId);
            e.Property(x => x.Code).HasMaxLength(20).IsRequired();
            e.Property(x => x.FullName).HasMaxLength(150).IsRequired();
            e.Property(x => x.Specialty).HasMaxLength(150);
            e.Property(x => x.Phone).HasMaxLength(20);
            e.Property(x => x.Email).HasMaxLength(256);
            e.HasIndex(x => new { x.ClinicId, x.Code }).IsUnique();

            e.HasOne(x => x.Clinic)
            .WithMany(c => c.Doctors)
            .HasForeignKey(x => x.ClinicId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Service>(e =>
        {
            e.ToTable("Services");
            e.HasKey(x => x.ServiceId);
            e.Property(x => x.Code).HasMaxLength(30).IsRequired();
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.HasIndex(x => new { x.ClinicId, x.Code }).IsUnique();

            e.HasOne(x => x.Clinic)
                .WithMany(c => c.Services)
                .HasForeignKey(x => x.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DoctorService>(e =>
        {
            e.ToTable("DoctorServices");
            e.HasKey(x => new { x.ServiceId, x.DoctorId });

            e.HasOne(ds => ds.Service)
                .WithMany(s => s.DoctorServices)
                .HasForeignKey(ds => ds.ServiceId);

            e.HasOne(ds => ds.Doctor)
                .WithMany(p => p.DoctorServices)
                .HasForeignKey(ds => ds.DoctorId);
        });

        modelBuilder.Entity<DoctorAvailability>(e =>
        {
            e.ToTable("DoctorAvailability");
            e.HasKey(x => x.AvailabilityId);
            e.Property(x => x.DayOfWeek).IsRequired();
            e.Property(x => x.StartTime).HasColumnType("time(0)");
            e.Property(x => x.EndTime).HasColumnType("time(0)");
            e.Property(x => x.SlotSizeMin).HasDefaultValue((short)30);
            e.HasIndex(x => new { x.DoctorId, x.DayOfWeek }).HasDatabaseName("IX_Avail_DoctorDow");

            e.HasOne(x => x.Clinic)
                .WithMany(c => c.DoctorAvailabilities)
                .HasForeignKey(x => x.ClinicId);

            e.HasOne(x => x.Doctor)
                .WithMany(p => p.DoctorAvailabilities)
                .HasForeignKey(x => x.DoctorId);
        });

        modelBuilder.Entity<Booking>(e =>
        {
            e.ToTable("Bookings");
            e.HasKey(x => x.BookingId);
            e.Property(x => x.FullName).HasMaxLength(150).IsRequired();
            e.Property(x => x.Phone).HasMaxLength(20).IsRequired();
            e.Property(x => x.Email).HasMaxLength(256);
            e.Property(x => x.Channel).HasMaxLength(10).HasDefaultValue("Web");
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).HasDefaultValue(BookingStatus.Pending);
            e.HasIndex(x => new { x.ClinicId, x.Status, x.CreatedAt }).HasDatabaseName("IX_Bookings_List");

            e.HasOne(x => x.Clinic)
                .WithMany(c => c.Bookings)
                .HasForeignKey(x => x.ClinicId);

            e.HasOne(x => x.Doctor)
                .WithMany(p => p.Bookings)
                .HasForeignKey(x => x.DoctorId);

            e.HasOne(x => x.Service)
                .WithMany(s => s.Bookings)
                .HasForeignKey(x => x.ServiceId);
        });

        modelBuilder.Entity<BookingToken>(e =>
        {
            e.ToTable("BookingTokens");
            e.HasKey(x => new { x.BookingId, x.Action });
            e.Property(x => x.Action).HasMaxLength(15).IsRequired();
            e.Property(x => x.Token).HasMaxLength(64).IsRequired();
            e.HasIndex(x => x.Token).IsUnique();

            e.HasOne(x => x.Booking)
                .WithMany(bk => bk.Tokens)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Appointment>(e =>
        {
            e.ToTable("Appointments");
            e.HasKey(x => x.AppointmentId);
            e.Property(x => x.Source).HasMaxLength(30).HasDefaultValue("Web");
            e.Property(x => x.ContactFullName).HasMaxLength(150).IsRequired();
            e.Property(x => x.ContactPhone).HasMaxLength(20).IsRequired();
            e.Property(x => x.ContactEmail).HasMaxLength(256);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).HasDefaultValue(AppointmentStatus.Confirmed);

            e.HasIndex(x => new { x.ClinicId, x.StartAt, x.EndAt }).HasDatabaseName("IX_Appt_Time");

            e.HasIndex(x => new { x.ClinicId, x.DoctorId, x.StartAt })
                .IsUnique()
                .HasFilter("\"Status\" NOT IN ('Cancelled','NoShow')")
                .HasDatabaseName("UX_Appt_Doctor");

            e.HasOne(x => x.Clinic)
                .WithMany(c => c.Appointments)
                .HasForeignKey(x => x.ClinicId);

            e.HasOne(x => x.Doctor)
                .WithMany(p => p.Appointments)
                .HasForeignKey(x => x.DoctorId);

            e.HasOne(x => x.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(x => x.ServiceId);

            e.HasOne(x => x.Booking)
                .WithOne(bk => bk.Appointment)
                .HasForeignKey<Appointment>(x => x.BookingId)
                .OnDelete(DeleteBehavior.SetNull);
        });

    }
}
