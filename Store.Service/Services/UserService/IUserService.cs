using Store.Service.Services.UserService.Dto;

namespace Store.Service.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto input);
        Task<UserDto> Login(LoginDto input);
    }
}
