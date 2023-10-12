using System.Linq.Expressions;
using Application.Domain.PersonAgregat;

namespace Application.Interfaces;

public interface IPersoneRepository
{
    Task<Person?> GetByIdAsync(Guid id);

    Task<Person?> GetOrDefaultAsync(Expression<Func<Person, bool>> predicate);
    Task<IReadOnlyList<Person>> ListAsync();
    Task<IReadOnlyList<Person>> ListAsync(Expression<Func<Person, bool>> predicate);
     
    /// <summary>
    /// Id уже есть в БД - выполняем Replace entity, возвращаем id измененной сущности или null если небыло изменений
    /// Id нет в БД - выполняем Add entity, возвращаем id сгенерированный на стороне БД.
    /// </summary>
    Task<Guid?> AddOrReplace(Person entity);
    
    Task<bool> DeleteAsync(Guid id);
    Task<long> DeleteAsync(Expression<Func<Person, bool>> predicate);
    Task<bool> IsExistAsync(Expression<Func<Person, bool>> predicate);
}