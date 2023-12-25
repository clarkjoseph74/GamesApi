using GamesApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Repositories;
public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Game> Games { get; }
    IBaseRepository<Category> Categories { get; }
    IBaseRepository<Platform> Platforms { get; }
    IBaseRepository<GamePlatform> GamePlatforms { get; }
    int Complete();
}
