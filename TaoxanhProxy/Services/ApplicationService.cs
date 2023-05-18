using Microsoft.Extensions.Caching.Memory;
using System;
using TaoxanhProxy.Models;

namespace TaoxanhProxy.Services
{
    public interface IApplicationService
    {
        public UserModel? CurrentUser { get; set; }
        public ProxyDeviceModel? CurrentDevice { get; set; }
    }
    public class ApplicationService : IApplicationService
    {
        const string UserKey = "user";
        const string DeviceKey = "device";
        private readonly IMemoryCache memory;

        public ApplicationService(IMemoryCache memory)
        {
            this.memory = memory;
        }


        public UserModel? CurrentUser
        {
            get => memory.Get<UserModel>(UserKey);
            set
            {
                if (value == null)
                {
                    memory.Remove(UserKey);
                    return;
                }
                memory.Set<UserModel>(UserKey, value, DateTimeOffset.UtcNow.AddSeconds(value.ExpiresIn));
            }
        }

        public ProxyDeviceModel? CurrentDevice
        {
            get => memory.Get<ProxyDeviceModel>(DeviceKey);
            set
            {
                if (value == null)
                {
                    memory.Remove(DeviceKey);
                    return;
                }
                memory.Set<ProxyDeviceModel>(DeviceKey, value, DateTimeOffset.UtcNow.AddYears(100));
            }
        }
    }
}
