using Microsoft.EntityFrameworkCore;
using Users.API.Infrastructure.Data;
using Users.API.Services.Models;

namespace Users.API.Services;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByDocumentAsync(string document);
}

public class UserRepository(ApplicationDataContext dataContext) : IUserRepository
{
    private readonly DbSet<User> _users = dataContext.Users;

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _users.FindAsync(id);
    }

    public async Task<User?> GetByDocumentAsync(string document)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Document == document);
    }
}
