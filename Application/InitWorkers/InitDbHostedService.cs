using Application.Domain.PersonAgregat;
using Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.InitWorkers;

/// <summary>
/// Сервис инициализации БД.
/// </summary>
public class InitDbHostedService: IHostedService
{
    private readonly IPersoneRepository _personeRepository;
    private readonly ILogger<InitDbHostedService> _logger;

    public InitDbHostedService(IPersoneRepository personeRepository, ILogger<InitDbHostedService> logger)
    {
        _personeRepository = personeRepository;
        _logger = logger;
    }
    
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var rootPersone = Person.Create("root", "123456", new Role("admin")).Value;
        try
        {
            var isExistRoot = await _personeRepository.IsExistAsync(person => 
                person.Role == rootPersone.Role &&
                person.Name == rootPersone.Name &&
                person.Password == rootPersone.Password);

            if (!isExistRoot)
            {
                var rootPersoneId= await _personeRepository.AddOrReplace(rootPersone);
                _logger.LogInformation("root пользователь добавлен {RootPersoneId}", rootPersoneId);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Ошибка инициализации БД");
            throw;
        }
        _logger.LogInformation("БД инициализирована Успешно");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}