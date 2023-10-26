using Application.Interfaces;
using MongoDB.Driver;
using MongoDBMigrations;

namespace Application.Infrastructure.Persistence.MongoDb;

public class MongoDbMigrationService : IDbMigrationService
{
    private readonly IMongoDatabase _mongoDatabase;

    private IMigrationRunner? _migrationRunner;
    
    public MongoDbMigrationService(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }
    
    public MigrationDbResult? RunMigrate()
    {
        try
        {
            _migrationRunner ??= new MigrationEngine()
                .UseDatabase(_mongoDatabase.Client, _mongoDatabase.DatabaseNamespace.DatabaseName)
                .UseAssemblyOfType<_1_0_0_ChangeNameMigration>()
                .UseSchemeValidation(false);

            var res = _migrationRunner.Run();
            if (res == null) {
                return null;
            }
            var migrationDbRes = new MigrationDbResult()
            {
                CurrentVersion = res.CurrentVersion,
                InterimSteps = res.InterimSteps.Select(step=> $"'{step.CurrentNumber}' 'V= {step.TargetVersion}' 'MN= {step.MigrationName}'").ToList(),
                DatabaseName = res.DatabaseName,
                Success = res.Success,
                ServerAdress = res.ServerAdress
            };
            
            return migrationDbRes;
        }
        catch (MigrationNotFoundException ex)
        {
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e); //Возвращать Result<T>
            throw;
        }
    }
}