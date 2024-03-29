using CRM_WebApp.Models;

namespace CRM_WebApp.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto data);
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto data);
        Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto data);
    }
}
