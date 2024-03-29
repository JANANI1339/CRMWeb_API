using CRM_WebApp.Models;
using CRM_WebApp.Service.IService;
using CRMWeb_API.Models;
using Newtonsoft.Json;

namespace CRM_WebApp.Service
{
    public class AuthService : IAuthService
    {
        HttpClient client = new HttpClient();
        string url = "https://localhost:7059/api/AuthAPI/";
        public async Task<ResponseDto> AssignRoleAsync(RegistrationRequestDto data)
        {
            var response = await client.PostAsJsonAsync(url + "AssignRole", data);
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(await response.Content.ReadAsStringAsync());
            return responseDto;
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto data)
        {
            var response = await client.PostAsJsonAsync(url + "login", data);
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(await response.Content.ReadAsStringAsync());
            return responseDto;
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto data)
        {
            var response = await client.PostAsJsonAsync(url + "register", data);
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(await response.Content.ReadAsStringAsync());
            return responseDto;
        }
    }
}
