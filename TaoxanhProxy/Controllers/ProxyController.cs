
using Microsoft.AspNetCore.Mvc;
using TaoxanhProxy.Models;
using TaoxanhProxy.Services;

namespace TaoxanhProxy.Controllers
{
    [ApiController]
    [Route("")]

    public class ProxyController : ControllerBase
    {
        private readonly IInternalService internalService;

        public ProxyController(IInternalService internalService)
        {
            this.internalService = internalService;
        }

        [HttpPost("check-proxy")]
        public async Task<bool?> CheckAsync(SetProxyDeviceRequestDto request)
        {
            return (await internalService.SetProxyForDeviceAsync(request.DeviceId)) != null;
        }
    }
}
