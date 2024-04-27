using Domain;
using Domain.Dto;
using Domain.Entities;

namespace Services.Contracts;

public interface IAdminService
{
    Task<Result<List<UserDto>>> GetAllUsersAsync();
    Task<Result<CreateUserDto>> CreateUserAsync(User user);
    Task<Result<CreateUserDto>> CreateAdminUserAsync();
}