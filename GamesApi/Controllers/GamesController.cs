using GamesApi.Core.Dtos;
using GamesApi.Core.Models;
using GamesApi.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GamesApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CloudinaryService _cloudinaryService;

    public GamesController(IUnitOfWork unitOfWork, CloudinaryService cloudinaryService)
    {

        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Game> items = _unitOfWork.Games.GetAll().ToList();
        List<GameDto> gamesList = new List<GameDto>();
        foreach (var item in items)
        {
            gamesList.Add(new GameDto { Id = item.Id, Name = item.Name, Description = item.Description,
                ImageUrl = item.imageUrl, Platforms = item.GamePlatforms.Select(x => x.Platform.Name).ToList(),
                category = item.CategoryType.Name
            });
        }
        return Ok(gamesList);
    }
    [HttpGet("platform/{platformId}")]
    public IActionResult GetByPlatform(int platformId)
    {
        return Ok(_unitOfWork.Games.GetAll(platformId));
    }

    [HttpGet("category/{categoryId}")]
    public IActionResult GetByCategory(int categoryId)
    {

        List<Game> items = _unitOfWork.Games.GetAll(x => x.CategoryId == categoryId).ToList();
        List<GameDto> gamesList = new List<GameDto>();
        foreach (var item in items)
        {
            gamesList.Add(new GameDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Platforms = item.GamePlatforms.Select(x => x.Platform.Name).ToList(),
                ImageUrl = item.imageUrl,
                category = item.CategoryType.Name
            });
        }
        return Ok(gamesList);
    }

    [HttpGet("single/{id}")]
    public IActionResult GetById(int id)
    {
        var game = _unitOfWork.Games.GetById(id);
        if (game == null)
        {
            return NotFound($"The game with id {id} not found");
        }
        return Ok(game);
    }

    [HttpPost]
    public IActionResult CreateGame([FromForm] GameCreateDto model)
    {


        if (model == null || model.platformIds == null || model.platformIds.Count() == 0)
        {
            return BadRequest("Invalid input");
        }

        if (model.Image == null || model.Image.Length <= 0)
            return BadRequest("Invalid file");

        var imageUrl = _cloudinaryService.UploadImage(model.Image);

        Game gameToCreate = new Game { Name = model.Name , Description = model.Description ,imageUrl = imageUrl , CategoryId = model.CategoryId }; 
        var game = _unitOfWork.Games.Create(gameToCreate); 
        if (game == null)
        {
            return NotFound($"Error while Creating the game");
        }
        _unitOfWork.Complete();
        JsonDocument jsonDocument = JsonDocument.Parse(model.platformIds);
        var arrayEnumerator = jsonDocument.RootElement.EnumerateArray();
        List<int> listOfIntegers = arrayEnumerator.Select(jsonValue => jsonValue.GetInt32()).ToList();
        foreach (var platformId in listOfIntegers)
        {
         

            var platform = _unitOfWork.Platforms.GetById(platformId);
            if (platform != null)
            {
                var gamePlatform = new GamePlatform { GameId = game.Id , PlatformId = platform.Id };
                _unitOfWork.GamePlatforms.Create(gamePlatform);
            }
            _unitOfWork.Complete();
        }
        _unitOfWork.Complete();
       GameDto dto = new GameDto() { Id = game.Id , Description = game.Description  , ImageUrl = game.imageUrl , Name = game.Name ,
           Platforms = game.GamePlatforms.Select(x => x.Game.Name).ToList() }; 
        return Ok(game);
    }

    [HttpPut("{gameId}")]
    public IActionResult UpdateGame(GameCreateDto model , int gameId)
    {
        Game gameToUpdate = new Game { Name = model.Name, Description = model.Description , Id = gameId };
        var game = _unitOfWork.Games.Update( gameToUpdate);
        _unitOfWork.Complete();
        if (game == null)
        {
            return NotFound($"Error while Updating the game");
        }
        return Ok(game);
    }

    [HttpDelete("{gameId}")]
    public IActionResult DeleteGame(int gameId)
    {
        var game = _unitOfWork.Games.Delete(gameId);
        _unitOfWork.Complete();
        if (game == null)
        {
            return NotFound($"Error while Deleting the game");
        }
        return Ok(game);
    }
}
