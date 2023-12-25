using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Dtos;
public class GameDto
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string category { get; set; }
    public List<string> Platforms { get; set; }
}
