using Domain;
using Domain.Dto;

namespace Authentication.Contracts;

public interface IUserAuthenticationService
{
    Task<Result<LoginDto>> LogIn(string username,  string password);
    Task<Result<LoginDto>> Register(string identifier, string username,  string password);
}