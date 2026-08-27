using Erpyonetimi.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Erpyonetimi.Services
{
    public partial class SQLService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _connectionStatus;
        public SQLService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    using var dbContext = scope.ServiceProvider.GetRequiredService<ErpDbContext>();
                    var status = await dbContext.Database.CanConnectAsync(stoppingToken);

                    if (_connectionStatus != status)
                    {
                        _connectionStatus = status;
                        
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        
    }
}
