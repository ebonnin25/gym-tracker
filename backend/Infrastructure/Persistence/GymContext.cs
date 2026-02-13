using Microsoft.EntityFrameworkCore;
using backend.Domain.Entities;

namespace backend.Infrastructure.Persistence;

public class GymContext : DbContext
{
    public GymContext(DbContextOptions<GymContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<Muscle> Muscles => Set<Muscle>();
    public DbSet<ExerciseMuscle> ExerciseMuscles => Set<ExerciseMuscle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExerciseMuscle>()
            .HasKey(em => new { em.ExerciseId, em.MuscleId });

        modelBuilder.Entity<ExerciseMuscle>()
            .HasOne(em => em.Exercise)
            .WithMany(e => e.ExerciseMuscles)
            .HasForeignKey(em => em.ExerciseId);

        modelBuilder.Entity<ExerciseMuscle>()
            .HasOne(em => em.Muscle)
            .WithMany(m => m.ExerciseMuscles)
            .HasForeignKey(em => em.MuscleId);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // seed muscles with fixed guid
        modelBuilder.Entity<Muscle>().HasData(
            new Muscle { Id = Guid.Parse("bae51a45-0eb0-4e51-97ae-e70bbe009eb6"), Name = "Pectoraux" },
            new Muscle { Id = Guid.Parse("c8498283-5483-491f-adbc-9dad4d591196"), Name = "Dos" },
            new Muscle { Id = Guid.Parse("66cefe28-52e3-46bc-8fc6-2c52090b1922"), Name = "Épaules" },
            new Muscle { Id = Guid.Parse("9a1fda82-7bcb-432f-85b6-d79c9bee783d"), Name = "Biceps" },
            new Muscle { Id = Guid.Parse("992bf433-2f92-4569-ac29-85b8f7066ff9"), Name = "Triceps" },
            new Muscle { Id = Guid.Parse("fcb9abb3-2715-4d5a-b0db-30c82dc1f60a"), Name = "Quadriceps" },
            new Muscle { Id = Guid.Parse("cf5d0331-4cca-4b0e-887d-8f453cf497a3"), Name = "Ischios" },
            new Muscle { Id = Guid.Parse("9bb5a066-0706-4dd1-8faa-a07700e5a50f"), Name = "Fessiers" },
            new Muscle { Id = Guid.Parse("62cc5531-ebdb-4b8e-8227-505c300285cc"), Name = "Mollets" },
            new Muscle { Id = Guid.Parse("6b93f3f6-73bb-4147-83c3-401c5a698b2a"), Name = "Abdos" },
            new Muscle { Id = Guid.Parse("7490b915-3115-4898-b86a-1855c095e814"), Name = "Lombaires" }
    );
    }
}