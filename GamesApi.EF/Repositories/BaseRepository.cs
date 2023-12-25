using GamesApi.Core.Dtos;
using GamesApi.Core.Models;
using GamesApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.EF.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    
    #region fields
        private readonly ApplicationDbContext _context;
    #endregion
    #region Constructor
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    #endregion

    #region Function Handler
    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? criteria = null)
            {
        IQueryable<T> query = _context.Set<T>();
                   if(criteria != null) 
                      query = query.Where(criteria);
                   return query.ToList();
            }

    public T? GetById(int id)
    {
        var result = _context.Set<T>().Find(id);
        if (result == null)
            return null;
        return result;
    }
    public T? Create(T entity)
    {
        var result = _context.Set<T>().Add(entity);
        if (result == null)
            return null;
        return entity;
    }
    public T? Update(T entity )
    {
        var itemToUpdate = _context.Set<T>().Update(entity);
            if (itemToUpdate == null)
            return null;
        return entity;
    }

    public T? Delete(int id)
    {
        var itemToDelete = _context.Set<T>().Find(id);
        if (itemToDelete == null)
            return null;
        _context.Set<T>().Remove(itemToDelete);
        return itemToDelete;
    }

    public IEnumerable<GameDto> GetAll(int platformId)
    {
        IEnumerable<GameDto> games = _context.GamesPlatforms.Where(gp => gp.PlatformId == platformId).Select(
            gp => new GameDto
            {
                Id = gp.Game.Id,
                Name = gp.Game.Name,
                Description = gp.Game.Description,
                Platforms = gp.Game.GamePlatforms.Select(x => x.Platform.Name).ToList()
            }
            )
            ;
        return games.ToList();
    }

    #endregion

}
