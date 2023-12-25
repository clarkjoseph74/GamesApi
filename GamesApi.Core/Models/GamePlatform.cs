using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Models;
public class GamePlatform
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int PlatformId { get; set; }

    public virtual Game Game { get; set; }
    public virtual Platform Platform { get; set; }
}