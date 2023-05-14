namespace WebApplication1;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
                => modelBuilder.ApplyConfiguration(new BookConfigration())
                                .ApplyConfiguration(new UserConfigration())
								.ApplyConfiguration(new RefreshTokenConfigration());
}