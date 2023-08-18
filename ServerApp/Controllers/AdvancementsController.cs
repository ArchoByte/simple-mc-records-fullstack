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
public class AdvancementsController : ControllerBase
{
    private readonly IAdvancementRepository _advancementRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper<Advancement, AdvancementDto> _mapper;

    public AdvancementsController(IAdvancementRepository advancementRepo, ICategoryRepository categoryRepo, IMapper<Advancement, AdvancementDto> mapper)
    {
        _advancementRepo = advancementRepo;
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    // GET: api/Advancements
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<AdvancementDto>>> GetAdvancements() => Ok(await _mapper.MapList((await _advancementRepo.GetAdvancementsAsync())!));

    // GET: api/Advancements/John
    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdvancementDto>> GetAdvancement(string name)
    {
        var advancement = await _advancementRepo.GetAdvancementByNameAsync(name);

        if (advancement == null)
            return NotFound(new Error() { Message = "Advancement wasn't found" });

        return Ok(await _mapper.Map(advancement));
    }

    // PUT: api/Advancements/John
    [HttpPut("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAdvancement(string name, AdvancementDto dto)
    {
        if (dto.Name == null)
            return BadRequest(new Error() { Message = "Name cannot be null" });

        if (dto.CategoryName == null)
            return BadRequest(new Error() { Message = "CategoryName cannot be null" });

        var advancement = await _advancementRepo.GetAdvancementByNameAsync(name);
        if (advancement == null)
            return NotFound(new Error() { Message = "Advancement wasn't found" });

        if (!_categoryRepo.CategoryExistsByName(dto.CategoryName))
            return NotFound(new Error() { Message = "Category wasn't found" });

        advancement.Name = dto.Name;
        advancement.Category = await _categoryRepo.GetCategoryByNameAsync(dto.CategoryName);

        if (!await _advancementRepo.PutAdvancementAsync(advancement))
            return NotFound(new Error() { Message = "Failed to update advancement" });

        return NoContent();
    }

    // POST: api/Advancements
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AdvancementDto>> PostAdvancement(AdvancementDto dto)
    {
        if (dto.Name == null)
            return BadRequest(new Error() { Message = "Name cannot be null" });

        if (dto.CategoryName == null)
            return BadRequest(new Error() { Message = "CategoryName cannot be null" });

        var advancement = await _advancementRepo.GetAdvancementByNameAsync(dto.Name);
        if (advancement != null)
            return Conflict(new Error() { Message = "Advancement with such name already exist" });

        // Check if Category with such name exist
        if (!_categoryRepo.CategoryExistsByName(dto.CategoryName))
        {
            var category = new Category() { Name = dto.CategoryName };
            await _categoryRepo.PostCategoryAsync(category);
        }
        var player = await _mapper.Map(dto);

        await _advancementRepo.PostAdvancementAsync(player);

        return CreatedAtAction(nameof(GetAdvancement), dto);
    }

    // DELETE: api/Advancements/John
    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAdvancement(string name)
    {
        var id = await _advancementRepo.GetIdByName(name);
        if (id == 0 || !await _advancementRepo.DeleteAdvancementAsync(id))
            return NotFound(new Error() { Message = "Advancement wasn't found" } );

        return NoContent();
    }
}