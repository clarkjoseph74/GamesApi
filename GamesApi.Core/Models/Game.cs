using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Models;
public class Game
{
    [Key]
    public int Id { get; set; }
    [Required , MaxLength(80)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string imageUrl { get; set; }

    [ForeignKey(nameof(CategoryType))]
    public int CategoryId { get; set; }
    public virtual Category CategoryType { get; set; }
    public virtual ICollection<GamePlatform> GamePlatforms { get; set; }

}
