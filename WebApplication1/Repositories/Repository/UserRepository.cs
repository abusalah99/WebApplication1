namespace WebApplication1;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public override async Task<User> Get(Guid id)
        => await dbSet.Include(e => e.Token).FirstOrDefaultAsync(e => e.Id == id);
    public override async Task<IEnumerable<User>> Get()
        => await dbSet.Include(e => e.Token).ToListAsync();

    public async Task<User>? GetByMail(string mail)
        => await dbSet.Include(e => e.Token).FirstOrDefaultAsync(e => e.Email == mail);

    public async Task DeleteByMail(string mail)
    {
        User? userFromDb = await GetByMail(mail);
        if(userFromDb == null)
            throw  new ArgumentException("user was not found");

        await Task.Run(()=>dbSet.Remove(userFromDb));
        await SaveChangesAsync();
    }

    public async Task<User>? GetByToken(string token)
    {
        if (token == null)
            throw new ArgumentException("Invalid Token");

        return await dbSet.Include(e => e.Token).FirstOrDefaultAsync(e => e.Token.Value == token);
    }
    public async Task<IEnumerable<User>>? GetUsersCreatedToday()
         => await dbSet.Where(e => e.CreatedAt.Date == DateTime.UtcNow.Date).ToListAsync();
    

    public async Task<IEnumerable<User>>? GetUsersCreatedAtMonth(int month , int year)
        => await dbSet.Where(e => e.CreatedAt.Month== month && e.CreatedAt.Year == year).ToListAsync();

}