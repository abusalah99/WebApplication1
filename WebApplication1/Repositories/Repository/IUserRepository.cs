namespace WebApplication1;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User> GetByMail(string mail);
    Task DeleteByMail(string mail);
    Task<User>? GetByToken(string token);
    Task<IEnumerable<User>>? GetUsersCreatedToday();

    Task<IEnumerable<User>>? GetUsersCreatedAtMonth(int month, int year);

}
