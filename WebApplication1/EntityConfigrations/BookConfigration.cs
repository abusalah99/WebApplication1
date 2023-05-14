namespace WebApplication1;

public class BookConfigration : BaseConfigration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.CoverImage).IsRequired();

        builder.Property(e => e.Pages).IsRequired();

        builder.Property(e => e.ReleaseDate).IsRequired();

        builder.Property(e => e.Author).IsRequired();
    }
}