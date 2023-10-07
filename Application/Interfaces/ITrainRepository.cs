using System.Linq.Expressions;
using Application.Domain;

namespace Application.Interfaces;

public interface ITrainRepository
{
     Task<Train?> GetByIdAsync(Guid id);
     Task<Train> GetSingleAsync(Expression<Func<Train, bool>> predicate);
     Task<IReadOnlyList<Train>> ListAsync();
     Task<IReadOnlyList<Train>> ListAsync(Expression<Func<Train, bool>> predicate);
     
     /// <summary>
     /// Id уже есть в БД - выполняем Replace entity, возвращаем id измененной сущности или null если небыло изменений
     /// Id нет в БД - выполняем Add entity, возвращаем id сгенерированный на стороне БД.
     /// </summary>
     Task<Guid?> AddOrReplace(Train entity);
     Task AddRangeAsync(IReadOnlyList<Train> entitys); 
     
     Task<bool> DeleteAsync(Guid id);
     Task<long> DeleteAsync(Expression<Func<Train, bool>> predicate);
     
     Task<bool> IsExistAsync(Expression<Func<Train, bool>> predicate);
}