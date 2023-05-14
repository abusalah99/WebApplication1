namespace WebApplication1;

public class BaseConfigrationSettings<TEntity> : BaseConfigration<TEntity>, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntitySettings
{
    public new virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Titel).IsRequired();
        builder.Property(e => e.Titel).HasMaxLength(20);
    }

}