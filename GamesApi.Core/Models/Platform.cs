using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Models;
public  class Platform
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<GamePlatform> GamePlatforms { get; set; }
}
