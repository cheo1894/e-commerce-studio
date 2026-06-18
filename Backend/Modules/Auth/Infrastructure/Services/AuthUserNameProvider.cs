using Backend.Modules.Auth.Infrastructure.Persistence;
using Backend.Modules.ProductCatalog.Application.Interfaces;

public class AuthUserNameProvider : IuserNameProvider
{

    private readonly AuthDbContext _context;


    public AuthUserNameProvider(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<string> GetUserNameByIdAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        return user?.UserName ?? "";
    }
}