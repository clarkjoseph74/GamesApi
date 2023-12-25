using GamesApi.Core.Models;
using GamesApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.EF.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepository<Game> Games { get; private set; }
    public IBaseRepository<Category> Categories { get; private set; }
    public IBaseRepository<Platform> Platforms { get; private set; }
    public IBaseRepository<GamePlatform> GamePlatforms { get; private set; }
    public UnitOfWork(ApplicationDbContext context)
    {
       _context = context;
        Games = new BaseRepository<Game>(_context);
        Categories = new BaseRepository<Category>(_context);
        Platforms = new BaseRepository<Platform>(_context);
        GamePlatforms = new BaseRepository<GamePlatform>(_context);
    }
    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
