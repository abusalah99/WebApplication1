namespace WebApplication1;

public class BaseConfigrationSettings<TEntity> : BaseConfigration<TEntity>, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntitySettings
{
    public new virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Title).HasMaxLength(20);
    }

}