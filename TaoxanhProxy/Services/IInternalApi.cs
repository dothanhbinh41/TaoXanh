using Newtonsoft.Json;
using Refit;
using System.Text.Json.Serialization;
using TaoxanhProxy.Models;

namespace TaoxanhProxy.Services
{
    public interface IInternalApi
    {
        [Post("/api/auth/token")]
        Task<ApiResponse<TokenResponseDto>> ConnectTokenAsync([Body] SignInRequestDto request);

        [Get("/api/profile")]
        Task<ApiResponse<ProfileDto>> GetProfileAsync([Authorize] string accessToken);

        [Post("/api/proxy-device")]
        Task<ApiResponse<ProxyDeviceModel>> CreateDeviceProxyAsync([Authorize] string accessToken, [Body] DeviceProxyRequestDto request);

        [Post("/api/proxy-device/{id}/device")]
        Task<ApiResponse<DeviceModel>> SetProxyForDeviceAsync([Authorize] string accessToken, string id, [Body] SetProxyDeviceRequestDto request);
    }

    public class DeviceProxyRequestDto
    {
        [JsonPropertyName("pid")]
        public string PID { get; set; }
    }


    public class SetProxyDeviceRequestDto
    {
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }
    }
    public class SignInRequestDto
    {
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class ProfileDto
    {
        public string Token { get; set; }
    }

    public class TokenResponseDto
    {
        [JsonProperty("access_token")]
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("refresh_token")]
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
