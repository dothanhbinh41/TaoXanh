using Microsoft.Extensions.Caching.Memory;
using Refit;
using TaoxanhProxy.Models;
using static System.Formats.Asn1.AsnWriter;

namespace TaoxanhProxy.Services
{
    public interface IInternalService
    {
        Task<UserModel?> GetCurrentUserAsync();
        Task<bool> AuthAsync(string username, string password);
        Task LogoutAsync();

        Task<DeviceModel?> SetProxyForDeviceAsync(string deviceId);

    }

    public class InternalService : IInternalService
    {
        readonly IApplicationService applicationService;
        readonly IInternalApi api;
        public InternalService(IApplicationService applicationService)
        {
            api = RestService.For<IInternalApi>("https://api6.autofarmer.net");
            this.applicationService = applicationService;
        }

        public async Task<bool> AuthAsync(string username, string password)
        {
            var res = await api.ConnectTokenAsync(new SignInRequestDto { UserName = username, Password = password });
            if (string.IsNullOrEmpty(res.Content?.AccessToken))
            {
                return false;
            }
            var profileResponse = await api.GetProfileAsync(res.Content.AccessToken);
            if (profileResponse == null || profileResponse.Content == null)
            {
                return false;
            }

            applicationService.CurrentUser = new UserModel
            {
                Token = profileResponse.Content.Token,
                AccessToken = res.Content.AccessToken,
                UserName = username,
                ExpiresIn = res.Content.ExpiresIn
            };

            var device = await CreateProxyDeviceAsync("");
            applicationService.CurrentDevice = device;
            return true;
        }

        public Task<UserModel?> GetCurrentUserAsync()
        {
            return Task.FromResult(applicationService.CurrentUser);
        }

        public Task LogoutAsync()
        {
            applicationService.CurrentUser = null;
            applicationService.CurrentDevice = null;
            return Task.FromResult(true);
        }

        public async Task<DeviceModel?> SetProxyForDeviceAsync(string deviceId)
        {
            var user = applicationService.CurrentUser;
            if (user == null)
            {
                return null;
            }
            var currentDevice = applicationService.CurrentDevice;
            if (currentDevice == null)
            {
                return null;
            }
            var res = await api.SetProxyForDeviceAsync(user.AccessToken, currentDevice.Id, new SetProxyDeviceRequestDto
            {
                DeviceId = deviceId
            });
            if (res.Content == null)
            {
                throw new Exception(res?.Error?.Content);
            }
            return res?.Content;
        }

        async Task<ProxyDeviceModel?> CreateProxyDeviceAsync(string pid)
        {
            var user = applicationService.CurrentUser;
            if (user == null)
            {
                return null;
            }
            var res = await api.CreateDeviceProxyAsync(user.AccessToken, new DeviceProxyRequestDto
            {
                PID = pid,
            });
            return res?.Content;
        }
    }
}
