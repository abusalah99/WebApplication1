namespace WebApplication1;

public class RefreshTokenConfigration : BaseConfigration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Value).IsRequired().HasMaxLength(128);
        
        builder.Property(e => e.CreatedAt).IsRequired();

        builder.Property(e => e.ExpireAt).IsRequired();
    }
}
