namespace WebApplication1;

public interface IUserUnitOfWork : IBaseUnitOfWork<User>
    {
    Task<User> GetUserByMail(string mail);
    Task DeleteUserByMail(string mail);
    Task<Token> Login(LoginRequest request);
    Task<Token> Register(User user);
    Task<Token> Refresh(string refreshToken);
    Task Logout(string refreshToken);
    Task<User> Update(UserRequest user,Guid id);
    Task<Token> UpdatePassword(PasswordRequest password,Guid id);

}
