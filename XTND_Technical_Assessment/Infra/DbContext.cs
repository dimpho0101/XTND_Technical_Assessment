using Microsoft.EntityFrameworkCore;
using XTND_Technical_Assessment.Domain;

namespace XTND_Technical_Assessment.Infra;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TaskUser> TaskUsers => Set<TaskUser>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

    public DbSet<TaskItemStatus> TaskStatuses => Set<TaskItemStatus>();


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

            b.HasOne(x => x.TaskStatus)
             .WithMany()
             .HasForeignKey(x => x.TaskStatusId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TaskItemStatus>(b =>
        {
            b.ToTable("task_item_statuses");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(x => x.IsActive)
                .IsRequired();

            b.HasIndex(x => x.Name).IsUnique();

            b.HasData(
                new TaskItemStatus { Id = 1, Name = "Backlog", IsActive = true },
                new TaskItemStatus { Id = 2, Name = "In Progress", IsActive = true },
                new TaskItemStatus { Id = 3, Name = "Completed", IsActive = true }
            );
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

                entry.Entity.TaskCreatedAtUtc = utcNow;
                entry.Entity.TaskUpdatedAtUtc = utcNow;
            }

            if (entry.State == EntityState.Modified)
            {

                entry.Entity.TaskUpdatedAtUtc = utcNow;

                entry.Property(x => x.TaskCreatedAtUtc).IsModified = false;
            }
        }
    }
}