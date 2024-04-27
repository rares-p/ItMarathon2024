using Domain;
using Domain.Dto;
using Domain.Entities;

namespace Services.Contracts;

public interface IAdminService
{
    Task<Result<List<UserDto>>> GetAllUsersAsync();
    Task<Result<User>> CreateUserAsync(User user);
    Task<Result<User>> CreateUserAsync(string identifier);
}