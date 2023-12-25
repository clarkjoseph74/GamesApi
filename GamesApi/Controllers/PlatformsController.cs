using GamesApi.Core.Dtos;
using GamesApi.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PlatformsController( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public IActionResult GetPlatforms()
    {
        var plats = _unitOfWork.Platforms.GetAll().Select(p => new PlatformDto { Id = p.Id , Name = p.Name});
        
        return Ok(plats);
    }
}
