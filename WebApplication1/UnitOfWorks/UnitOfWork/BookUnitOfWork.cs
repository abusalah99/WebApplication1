namespace WebApplication1;

public class BookUnitOfWork : BaseSettingsUnitOfWork<BookEntity>, IBookUnitOfWork
    {
        private readonly IImageConverter _converter;
        public BookUnitOfWork(IBookRepsitory repository,
            ILogger<BookUnitOfWork> logger, IImageConverter converter)
            : base(repository, logger) => _converter = converter;

        public async Task Create(BookRequest request)
        {
            var sectionFromDb = await Search(request.Title);

            if (sectionFromDb.Any())
                throw new ArgumentException("This name is already used");

            if (request.CoverImage == null)
                throw new ArgumentException("Image was not supplied");

            string extension = Path.GetExtension(request.CoverImage.FileName);

            byte[] image = await _converter.ConvertImage(request.CoverImage);

            BookEntity homeSection = new()
            {
                Titel = request.Title,
                CoverImage = image,
                Pages = request.Pages,
                ReleaseDate = request.ReleaseDate
            };

            await Create(homeSection);
        }

        public async Task Update(BookRequest request)
        {
        BookEntity sectionFromDb = await Read(request.Id);

            if (sectionFromDb == null)
                throw new ArgumentException("Section not found");

            if (request.CoverImage != null)
            {
                string extension = Path.GetExtension(request.CoverImage.FileName);

                byte[] image = await _converter.ConvertImage(request.CoverImage);

                sectionFromDb.CoverImage = image;
                sectionFromDb.Pages = request.Pages ;
            }
            sectionFromDb.Titel = request.Title;
            sectionFromDb.ReleaseDate = request.ReleaseDate;

            await Update(sectionFromDb);
        }
    }
