using SimpleMcRecords.Dto;
using SimpleMcRecords.Interfaces;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Helper;

public class FullScoreMapper : IMapper<Score, FullScoreDto>
{
    private readonly IPlayerRepository _playerRepo;
    public FullScoreMapper(IPlayerRepository playerRepo)
    {
        _playerRepo = playerRepo;
    }
    public async Task<List<FullScoreDto>> MapList(ICollection<Score> list)
    {
        var newList = new List<FullScoreDto>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public Task<FullScoreDto> Map(Score model)
    {
        var dto = new FullScoreDto()
        {
            Name = model.Name,
            Value = model.Value
        };
        if (model.Player != null)
            dto.PlayerName = model.Player.Name;
        return Task.FromResult(dto);
    }
    public async Task<List<Score>> MapList(ICollection<FullScoreDto> list)
    {
        var newList = new List<Score>();
        foreach (var item in list)
            newList.Add(await Map(item));
        return newList;
    }
    public async Task<Score> Map(FullScoreDto dto)
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