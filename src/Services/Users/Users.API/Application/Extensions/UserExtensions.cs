using Users.API.Application.ViewModels;
using Users.API.Services.DTOs;
using Users.API.Services.Models;

namespace Users.API.Application.Extensions;

public static class UserExtensions
{
    public static IEnumerable<UserViewModel> ToViewModel(this IEnumerable<UserDTO> users)
    {
        return users.Select(ToViewModel);
    }

    public static UserViewModel ToViewModel(this UserDTO userDTO)
    {
        return new UserViewModel(userDTO.Id, userDTO.Name, userDTO.Document, userDTO.Email);
    }

    public static IEnumerable<UserDTO> ToDTO(this IEnumerable<User> users)
    {
        return users.Select(ToDTO);
    }

    public static UserDTO ToDTO(this User user)
    {
        return new UserDTO(user.Id, user.Name, user.Document, user.Email, user.Balance);
    }
}
