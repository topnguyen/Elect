namespace Elect.Web.Consul.HostedServices
{
    public class ConsulHostedService : IHostedService
    {
        private CancellationTokenSource _cts;
        private readonly IServer _server;
        private readonly ElectConsulOptions _consulConfig;
        private readonly ElectHealthCheckOptions _healthCheckConfig;
        private readonly IConsulClient _consulClient;
        private string _registrationId;
        private readonly ILogger _logger;
        public ConsulHostedService(IConsulClient consulClient,
            IOptions<ElectConsulOptions> consulConfig,
            IOptions<ElectHealthCheckOptions> healthCheckOptions,
            IServer server,
            ILoggerFactory loggerFactory)
        {
            _server = server;
            _consulConfig = consulConfig.Value;
            _healthCheckConfig = healthCheckOptions.Value;
            _logger = loggerFactory.CreateLogger<IHostedService>();
            _consulClient = consulClient;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_consulConfig.IsEnable)
            {
                return;
            }
            // Create a linked token so we can trigger cancellation outside of this token's cancellation
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var features = _server.Features;
            var address = _consulConfig.ServiceEndpoint;
            if (string.IsNullOrWhiteSpace(address))
            {
                var addresses = features.Get<IServerAddressesFeature>();
                _logger.Log(LogLevel.Information, $"Consul > Host All Address is: {string.Join(";", addresses.Addresses)}");
                address = addresses.Addresses.First();
                address = address.Replace("[::]", "127.0.0.1");
            }
            _logger.Log(LogLevel.Information, $"Consul > Config Service Endpoint is: {address}");
            var uri = new Uri(address);
            _registrationId = $"{_consulConfig.ServiceId}-{EnvHelper.MachineName}:{uri.Port}";
            var healthCheckPath = _healthCheckConfig.Endpoint.Trim().Replace("/", string.Empty);
            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _consulConfig.ServiceName.Replace(" ", "_"),
                Address = uri.Host,
                Port = uri.Port,
                Tags = _consulConfig.Tags.ToArray(),
                Check = new AgentServiceCheck()
                {
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/{healthCheckPath}",
                    Timeout = _consulConfig.CheckTimeOut,
                    Interval = _consulConfig.CheckInternal,
                    DeregisterCriticalServiceAfter = _consulConfig.DeregisterDeadServiceAfter,
                },
                EnableTagOverride = true
            };
            _logger.Log(LogLevel.Information, "Consul > Registering...");
            try
            {
                await _consulClient.Agent.ServiceRegister(registration, cancellationToken);
                _logger.Log(LogLevel.Information, "Consul > Registered!");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Consul > Register Failed!");
                _logger.Log(LogLevel.Error, $"Consul > {ex}");
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (!_consulConfig.IsEnable)
            {
                return;
            }
            _cts.Cancel();
            _logger.Log(LogLevel.Information, $"Consul > De-registering...");
            try
            {
                await _consulClient.Agent.ServiceDeregister(_registrationId, cancellationToken);
                _logger.Log(LogLevel.Information, $"Consul > Unregistered");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Consul > Unregister Failed!");
                _logger.Log(LogLevel.Error, $"Consul > {ex}");
            }
        }
    }
}
