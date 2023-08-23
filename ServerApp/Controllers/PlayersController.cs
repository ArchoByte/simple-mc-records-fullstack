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
public class PlayersController : ControllerBase
{
    private readonly IPlayerRepository _playerRepo;
    private readonly IAdvancementRepository _advancementRepo;
    private readonly IMapper<Player, PlayerDto> _mapper;

    public PlayersController(IPlayerRepository playerRepo, IAdvancementRepository advancementRepo, IMapper<Player, PlayerDto> mapper)
    {
        _playerRepo = playerRepo;
        _advancementRepo = advancementRepo;
        _mapper = mapper;
    }

    // GET: api/Players
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<PlayerDto>>> GetPlayers() => Ok(await _mapper.MapList((await _playerRepo.GetPlayersAsync())!));

    // GET: api/Players/John
    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlayerDto>> GetPlayer(string name)
    {
        var player = await _playerRepo.GetPlayerByNameAsync(name);

        if (player == null)
            return NotFound(new Error() { Message = "Player wasn't found" });

        return Ok(await _mapper.Map(player));
    }

    // POST: api/Players/John/Advancements/Kevin
    [HttpPost("{name}/Advancements/{advancementName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> PostAdvancement(string name, string advancementName)
    {
        var playerId = await _playerRepo.GetIdByName(name);

        if (playerId == 0)
            return NotFound(new Error() { Message = "Player wasn't found" });

        var advancementId = await _advancementRepo.GetIdByName(advancementName);

        if (advancementId == 0)
            return NotFound(new Error() { Message = "Advancement wasn't found" });

        if (_playerRepo.AdvancementExists(playerId, advancementId))
            return Conflict(new Error() { Message = "Advancement has already been achieved" });

        await _playerRepo.AddAdvancementAsync(playerId, advancementId);

        return Ok("Advancement was added to advancement list of player");
    }

    // DELETE: api/Players/John/Advancements/Kevin
    [HttpDelete("{name}/Advancements/{advancementName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlayerDto>> DeleteAdvancement(string name, string advancementName)
    {
        var playerId = await _playerRepo.GetIdByName(name);

        if (playerId == 0)
            return NotFound(new Error() { Message = "Player wasn't found" });

        var advancementId = await _advancementRepo.GetIdByName(advancementName);

        if (advancementId == 0)
            return NotFound(new Error() { Message = "Advancement wasn't found" });

        if (!_playerRepo.AdvancementExists(playerId, advancementId))
            return Conflict(new Error() { Message = "Advancement hasn't been achieved" });

        await _playerRepo.RemoveAdvancementAsync(playerId, advancementId);

        return Ok("Advancement was removed from advancement list of player");
    }

    // GET: api/Players/John/Advancements
    [HttpGet("{name}/Advancements")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICollection<CompletedAdvancementDto>>> GetAdvancements(string name)
    {
        var player = await _playerRepo.GetPlayerByNameAsync(name);

        if (player == null)
            return NotFound(new Error() { Message = "Player wasn't found" });

        var advancementDtoList = new List<CompletedAdvancementDto>();

        if (player.PlayerAdvancements == null)
            return advancementDtoList;

        foreach (var playerAdvancement in player.PlayerAdvancements)
        {
            var advancement = await _advancementRepo.GetAdvancementAsync(playerAdvancement.AdvancementId);
            advancementDtoList.Add(new CompletedAdvancementDto()
            {
                Name = advancement!.Name,
                CategoryName = advancement.Category!.Name,
                Time = playerAdvancement.Time
            });
        }

        return advancementDtoList;
    }

    // GET: api/Players/John/Scores
    [HttpGet("{name}/Scores")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICollection<ScoreDto>>> GetScores(string name)
    {
        var player = await _playerRepo.GetPlayerByNameAsync(name);

        if (player == null)
            return NotFound(new Error() { Message = "Player wasn't found" });

        var scoreDtoList = new List<ScoreDto>();

        if (player.Scores == null)
            return scoreDtoList;

        foreach (var score in player.Scores)
            scoreDtoList.Add(new ScoreDto()
            {
                Name = score.Name,
                Value = score.Value
            });

        return scoreDtoList;
    }

    // PUT: api/Players/John
    [HttpPut("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutPlayer(string name, PlayerDto dto)
    {
        var player = await _playerRepo.GetPlayerByNameAsync(name);
        if (player == null)
            return NotFound(new Error() { Message = "Player wasn't found" });

        player.Name = dto.Name;

        if (!await _playerRepo.PutPlayerAsync(player))
            return NotFound(new Error() { Message = "Failed to update player" });

        return NoContent();
    }

    // POST: api/Players
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlayerDto>> PostPlayer(PlayerDto dto)
    {
        if (dto.Name == null)
            return BadRequest(new Error() { Message = "Player.Name cannot be null" });

        if (_playerRepo.PlayerExistsByName(dto.Name))
            return Conflict(new Error() { Message = "Player with such name already exist" });

        await _playerRepo.PostPlayerAsync(await _mapper.Map(dto));

        return CreatedAtAction(nameof(GetPlayer), dto);
    }

    // DELETE: api/Players/John
    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePlayer(string name)
    {
        var id = await _playerRepo.GetIdByName(name);
        if (id == 0 || !await _playerRepo.DeletePlayerAsync(id))
            return NotFound(new Error() { Message = "Player wasn't found" });

        return NoContent();
    }
}
