using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesApi.Core.Dtos;
public class GameCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]

    public string Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public IFormFile Image {  get; set; }
    [Required]
    public string platformIds { get; set; }

}
