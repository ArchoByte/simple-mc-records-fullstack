using SimpleMcRecords.Dto;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Helper;

public class ScoreMapper : IMapper<Score, ScoreDto>
{
    private readonly IPlayerRepository _playerRepo;
    public ScoreMapper(IPlayerRepository playerRepo)
    {
        _playerRepo = playerRepo;
    }
    public async Task<List<ScoreDto>> MapList(ICollection<Score> list)
    {
        var newList = new List<ScoreDto>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<ScoreDto> Map(Score model)
    {
        var dto = new ScoreDto()
        {
            Name = model.Name,
            Value = model.Value
        };
        if (model.Player != null)
            dto.PlayerName = model.Player.Name;
        return Task.FromResult(dto);
    }
    public async Task<List<Score>> MapList(ICollection<ScoreDto> list)
    {
        var newList = new List<Score>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public async Task<Score> Map(ScoreDto dto)
    {
        var model = new Score()
        {
            Name = dto.Name,
            Value = dto.Value
        };
        var player = await _playerRepo.GetPlayerByNameAsync(dto.PlayerName!);
        if (player != null)
            model.Player = player;
        return model;
    }
}