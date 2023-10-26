using Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace Application.InitWorkers;

public class MigrateDbHostedService: IHostedService
{
    private readonly IDbMigrationService _migrationService;
    private readonly ILogger _logger;


    public MigrateDbHostedService(IDbMigrationService migrationService, ILogger logger)
    {
        _migrationService = migrationService;
        _logger = logger;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var res=_migrationService.RunMigrate();
        if (res == null)
        {
            _logger.Information("Миграции не найдены");
        }
        else
        if (res.Success)
        {
            _logger.Information("Миграция прошла успешно {MirgationResult}", res.ToString());
        }
        else
        {
            _logger.Error("Миграция закончилась ошибкой");
        }
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}