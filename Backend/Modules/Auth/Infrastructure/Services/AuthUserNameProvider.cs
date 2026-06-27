using Backend.Modules.Auth.Infrastructure.Persistence;
using Backend.Modules.ProductCatalog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AuthUserNameProvider : IuserNameProvider
{

    private readonly IDbContextFactory<AuthDbContext> _contextFactory;


    public AuthUserNameProvider(IDbContextFactory<AuthDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<string> GetUserNameByIdAsync(int userId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var user = await context.Users.FindAsync(userId);
        return user?.UserName ?? "";
    }
}