using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Models;
public class Category
{
    [Key]
    public int CatId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Game> Games { get; set; }

}
