using System.Linq.Expressions;
using Application.Domain;

namespace Application.Interfaces;

public interface IMetroTrainRepository
{
     Task<MetroTrain> GetByIdAsync(Guid id);
     Task<MetroTrain> GetSingleAsync(Expression<Func<MetroTrain, bool>> predicate);
     Task<IReadOnlyList<MetroTrain>> ListAsync();
     Task<IReadOnlyList<MetroTrain>> ListAsync(Expression<Func<MetroTrain, bool>> predicate);
     
     /// <summary>
     /// Id уже есть в БД - выполняем Replace entity, возвращаем id измененной сущности или null если небыло изменений
     /// Id нет в БД - выполняем Add entity, возвращаем null.
     /// </summary>
     Task<Guid?> AddOrReplace(MetroTrain entity);
     Task AddRangeAsync(IReadOnlyList<MetroTrain> entitys); 
     
     Task<bool> DeleteAsync(Guid id);
     Task<long> DeleteAsync(Expression<Func<MetroTrain, bool>> predicate);
     
     Task<bool> IsExistAsync(Expression<Func<MetroTrain, bool>> predicate);
}