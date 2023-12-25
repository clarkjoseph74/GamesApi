using GamesApi.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Repositories;
public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? criteria = null);
    T GetById(int id);

    T Create(T entity);

    T Update(T entity);
    T Delete(int id);

    IEnumerable<GameDto> GetAll(int platformId);


}
