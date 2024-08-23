using Users.API.Application.Extensions;
using Users.API.Services.DTOs;

namespace Users.API.Services;

public interface IUserService
{
    Task<UserDTO?> GetByIdAsync(int id);
    Task<UserDTO?> GetByDocumentAsync(string document);
}

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user?.ToDTO();
    }

    public async Task<UserDTO?> GetByDocumentAsync(string document)
    {
        var user = await _userRepository.GetByDocumentAsync(document);

        return user?.ToDTO();
    }
}
