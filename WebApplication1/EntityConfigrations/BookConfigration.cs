namespace WebApplication1;

public class BookConfigration : BaseConfigration<BookEntity>
{
    public override void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.CoverImage).IsRequired();

        builder.Property(e => e.Pages).IsRequired();

        builder.Property(e => e.ReleaseDate).IsRequired();

        builder.Property(e => e.Author).IsRequired();
    }
}