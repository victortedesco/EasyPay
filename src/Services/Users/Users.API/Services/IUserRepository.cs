using Microsoft.EntityFrameworkCore;
using Users.API.Infrastructure.Data;
using Users.API.Services.Models;

namespace Users.API.Services;

public interface IUserRepository
{
    Task<User?> AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByDocumentAsync(string document);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> SetUserBalance(Guid id, decimal balance);
}

public class UserRepository(ApplicationDbContext dataContext) : IUserRepository
{
    private readonly DbSet<User> _users = dataContext.Users;

    public async Task<User?> AddAsync(User user)
    {
        var entry = await _users.AddAsync(user);
        await dataContext.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _users.FindAsync(id);
    }

    public async Task<User?> GetByDocumentAsync(string document)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Document == document);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> SetUserBalance(Guid id, decimal balance)
    {
        var user = await _users.FindAsync(id);

        if (user is null)
            return false;

        user.Balance = balance;
        await dataContext.SaveChangesAsync();

        return true;
    }
}
