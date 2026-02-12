using Microsoft.EntityFrameworkCore;
using backend.Domain;

namespace backend.Infrastructure;

public class GymContext : DbContext
{
    public GymContext(DbContextOptions<GymContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
}
