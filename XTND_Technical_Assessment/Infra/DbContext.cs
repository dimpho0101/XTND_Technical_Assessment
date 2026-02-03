using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Domain;

namespace XTND_Technical_Assessment.Infra;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskUser> TaskUsers => Set<TaskUser>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TaskUser>(b =>
        {
            b.ToTable("task_users");
            b.HasKey(x => x.Id);

            b.Property(x => x.DisplayName)
             .HasMaxLength(200)
             .IsRequired();
        });

        modelBuilder.Entity<TaskItem>(b =>
        {
            b.ToTable("tasks");
            b.HasKey(x => x.Id);

            b.Property(x => x.Title)
             .HasMaxLength(200)
             .IsRequired();

            b.Property(x => x.TaskCreatedAtUtc)
             .IsRequired();

            b.Property(x => x.TaskUpdatedAtUtc)
             .IsRequired();

            b.HasOne(x => x.TaskUser)
             .WithMany(u => u.Tasks)
             .HasForeignKey(x => x.TaskUserId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }

    public override int SaveChanges()
    {
        ApplyTaskTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTaskTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTaskTimestamps()
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<TaskItem>())
        {
            if (entry.State == EntityState.Added)
            {
                // created + updated are SAME at insert time
                entry.Entity.TaskCreatedAtUtc = utcNow;
                entry.Entity.TaskUpdatedAtUtc = utcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                // only updated changes on update
                entry.Entity.TaskUpdatedAtUtc = utcNow;

                // protect created from being changed accidentally
                entry.Property(x => x.TaskCreatedAtUtc).IsModified = false;
            }
        }
    }
}