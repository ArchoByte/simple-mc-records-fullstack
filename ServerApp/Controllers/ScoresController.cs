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
public class ScoresController : ControllerBase
{
    private readonly IScoreRepository _scoreRepo;
    private readonly IPlayerRepository _playerRepo;
    private readonly IMapper<Score, ScoreDto> _mapper;
    private readonly IMapper<Score, FullScoreDto> _fullMapper;

    public ScoresController(IScoreRepository scoreRepo, IPlayerRepository playerRepo, IMapper<Score, ScoreDto> mapper, IMapper<Score, FullScoreDto> fullMapper)
    {
        _scoreRepo = scoreRepo;
        _playerRepo = playerRepo;
        _mapper = mapper;
        _fullMapper = fullMapper;
    }

    // GET: api/Scores
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<FullScoreDto>>> GetScores() => Ok(await _fullMapper.MapList((await _scoreRepo.GetScoresAsync())!));

    // GET: api/Scores/John/Players/Kevin
    [HttpGet("{name}/Players/{playerName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FullScoreDto>> GetScore(string name, string playerName)
    {
        var score = await _scoreRepo.GetScoreByNameAndPlayerAsync(name, playerName);

        if (score == null)
            return NotFound(new Error() { Message = "Score wasn't found" });
        
        return Ok(await _fullMapper.Map((await _scoreRepo.GetScoreByNameAndPlayerAsync(name, playerName))!));
    }

    // PUT: api/Scores/John/Players/Kevin
    [HttpPut("{name}/Players/{playerName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutScore(string name, string playerName, ScoreDto dto)
    {
        if (dto.Name == null)
            return BadRequest(new Error() { Message = "Name cannot be null" });
        
        if (!_playerRepo.PlayerExistsByName(playerName))
            return NotFound(new Error() { Message = "Player wasn't found" });
        
        var score = await _scoreRepo.GetScoreByNameAndPlayerAsync(name, playerName);
        if (score == null)
            return NotFound(new Error() { Message = "Score wasn't found" });

        score.Name = dto.Name;
        score.Value = dto.Value;

        if (!await _scoreRepo.PutScoreAsync(score))
            return NotFound(new Error() { Message = "Failed to update score" });

        return NoContent();
    }

    // POST: api/Scores
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<FullScoreDto>> PostScore(FullScoreDto dto)
    {
        if (dto.Name == null)
            return BadRequest(new Error() { Message = "Name cannot be null" });

        if (dto.PlayerName == null)
            return BadRequest(new Error() { Message = "PlayerName cannot be null" });
        
        if (!_playerRepo.PlayerExistsByName(dto.PlayerName))
            return NotFound(new Error() { Message = "Player wasn't found" });

        if (_scoreRepo.ScoreExistsByNameAndPlayer(dto.Name, dto.PlayerName))
            return Conflict(new Error() { Message = "Score with such name and player already exist" });

        await _scoreRepo.PostScoreAsync(await _fullMapper.Map(dto));

        return CreatedAtAction(nameof(GetScore), dto);
    }

    // DELETE: api/Scores/John/Players/Kevin
    [HttpDelete("{name}/Players/{playerName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteScore(string name, string playerName)
    {
        var id = await _scoreRepo.GetIdByNameAndPlayer(name, playerName);
        if (id == 0 || !await _scoreRepo.DeleteScoreAsync(id))
            return NotFound(new Error() { Message = "Score wasn't found" });

        return NoContent();
    }
}