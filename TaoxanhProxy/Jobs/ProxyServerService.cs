namespace TaoxanhProxy.Jobs
{
    public class ProxyServerService : IHostedService
    {
        readonly ProxyTestController proxyServer;
        public ProxyServerService()
        {
            proxyServer = new ProxyTestController();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            proxyServer.StartProxy(8000);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            proxyServer.Stop();
            return Task.CompletedTask;
        }
    }
}
