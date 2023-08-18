using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMcRecords.Models;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Dto;

namespace SimpleMcRecords.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper<Category, CategoryDto> _mapper;

    public CategoriesController(ICategoryRepository repository, IMapper<Category, CategoryDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // GET: api/Categories
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<CategoryDto>>> GetCategories() => Ok(await _mapper.MapList((await _repository.GetCategoriesAsync())!));

    // GET: api/Categories/John
    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetCategory(string name)
    {
        var category = await _repository.GetCategoryByNameAsync(name);

        if (category == null)
            return NotFound(new Error() { Message = "Category wasn't found" });

        return Ok(await _mapper.Map(category));
    }

    // PUT: api/Categories/John
    [HttpPut("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCategory(string name, CategoryDto dto)
    {
        if (dto.Name == null)
            return BadRequest(new Error() { Message = "Name cannot be null" });
        
        var category = await _repository.GetCategoryByNameAsync(name);
        if (category == null)
            return NotFound(new Error() { Message = "Category wasn't found" });
        
        category.Name = dto.Name;

        if (!await _repository.PutCategoryAsync(category))
            return NotFound(new Error() { Message = "Failed to update category" });

        return NoContent();
    }

    // POST: api/Categories
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto dto)
    {
        if (dto.Name == null)
            return BadRequest(new Error() { Message = "Name cannot be null" });

        var category = await _repository.GetCategoryByNameAsync(dto.Name);
        if (category != null)
            return Conflict(new Error() { Message = "Category with such name already exist" });

        await _repository.PostCategoryAsync(await _mapper.Map(dto));

        return CreatedAtAction(nameof(GetCategory), dto);
    }

    // DELETE: api/Categories/John
    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(string name)
    {
        var id = await _repository.GetIdByName(name);
        if (id == 0 || !await _repository.DeleteCategoryAsync(id))
            return NotFound(new Error() { Message = "Category wasn't found" });

        return NoContent();
    }
}