using GamesApi.Core.Dtos;
using GamesApi.Core.Models;
using GamesApi.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var cats = _unitOfWork.Categories.GetAll();
        List<CategoryDto> data = new List<CategoryDto>();
        foreach(var category in cats)
        {
            data.Add(new CategoryDto { Id = category.CatId , Name = category.Name});
        }
        return Ok(data);
    }
    [HttpPost]
    public IActionResult CreateCategory(CategoryCreateDto dto)
    {
        Category categoryToAdd = new Category
        {
            Name = dto.Name,
        };
        var cat = _unitOfWork.Categories.Create(categoryToAdd);
        if(cat != null)
        {
            _unitOfWork.Complete();
            CategoryDto result = new CategoryDto()
            {
                Id = cat.CatId,
                Name = cat.Name
            };
            
             return Ok(result);
        }
        return BadRequest("Error while creating the category !");
    }

    [HttpPut("{catId}")]
    public IActionResult UpdateCategory(int catId, CategoryCreateDto dto)
    {
        Category categoryToUpdate = new Category
        {
            CatId = catId,
            Name = dto.Name,
        };
        var cat = _unitOfWork.Categories.Update(categoryToUpdate);
        if (cat != null)
        {
            _unitOfWork.Complete();
            CategoryDto result = new CategoryDto()
            {
                Id = cat.CatId,
                Name = cat.Name
            };

            return Ok(result);
        }
        return BadRequest("Error while updating the category !");
    }
    [HttpDelete("{catId}")]
    public IActionResult DeleteCategory(int catId)
    {
        var cat = _unitOfWork.Categories.Delete(catId);
        if(cat != null )
        {
            _unitOfWork.Complete();
            return Ok(cat);
        }
        return BadRequest("Error while removing the category !");
    }
}
