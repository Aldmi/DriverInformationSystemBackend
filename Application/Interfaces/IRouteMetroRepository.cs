using System.Linq.Expressions;
using Application.Domain.RouteMetroAgregat;

namespace Application.Interfaces;

public interface IRouteMetroRepository
{
     Task<RouteMetro?> GetByIdAsync(Guid id);
     Task<RouteMetro> GetSingleAsync(Expression<Func<RouteMetro, bool>> predicate);
     Task<IReadOnlyList<RouteMetro>> ListAsync();
     Task<IReadOnlyList<RouteMetro>> ListAsync(Expression<Func<RouteMetro, bool>> predicate);
     
     /// <summary>
     /// Id уже есть в БД - выполняем Replace entity, возвращаем id измененной сущности или null если небыло изменений
     /// Id нет в БД - выполняем Add entity, возвращаем id сгенерированный на стороне БД.
     /// </summary>
     Task<Guid?> AddOrReplace(RouteMetro entity);
     Task AddRangeAsync(IReadOnlyList<RouteMetro> entitys); 
     
     Task<bool> DeleteAsync(Guid id);
     Task<long> DeleteAsync(Expression<Func<RouteMetro, bool>> predicate);
     Task<bool> IsExistAsync(Expression<Func<RouteMetro, bool>> predicate);
}