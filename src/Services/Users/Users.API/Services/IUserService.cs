using Users.API.Application.Extensions;
using Users.API.Services.DTOs;
using Users.API.Services.Models;

namespace Users.API.Services;

public interface IUserService
{
    Task<UserDTO?> AddAsync(UserDTO user);
    Task<UserDTO?> GetByIdAsync(Guid id);
    Task<UserDTO?> GetByDocumentAsync(string document);
    Task<UserDTO?> GetByEmailAsync(string email);
    Task<bool> SetUserBalance(Guid id, decimal balance);
}

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDTO?> AddAsync(UserDTO user)
    {
        if (await _userRepository.GetByIdAsync(user.Id) is not null)
            return null;

        if (await _userRepository.GetByEmailAsync(user.Email) is not null)
            return null;

        if (await _userRepository.GetByDocumentAsync(user.Document) is not null)
            return null;

        var model = new User { Id = user.Id, Document = user.Document, Email = user.Email, Name = user.Name, Balance = 0 };
        var newUser = await _userRepository.AddAsync(model);
        return newUser?.ToDTO();
    }

    public async Task<UserDTO?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user?.ToDTO();
    }

    public async Task<UserDTO?> GetByDocumentAsync(string document)
    {
        var user = await _userRepository.GetByDocumentAsync(document);

        return user?.ToDTO();
    }

    public async Task<UserDTO?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        return user?.ToDTO();
    }

    public async Task<bool> SetUserBalance(Guid id, decimal balance)
    {
        return await _userRepository.SetUserBalance(id, balance);
    }
}
